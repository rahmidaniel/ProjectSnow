using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Utility.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TreeManager : MonoBehaviour, IPersistentData
    {
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private int maxTrees = 10;
        [SerializeField, Range(0, 1)] private float density;
        private BoxCollider2D _area;
        private List<GameObject> _trees;
        private List<Vector2> _points; // will be serialized

        private BoxCollider2D _treeCollider;

        private void Start()
        {
            _area = GetComponent<BoxCollider2D>();
            _trees = new List<GameObject>(maxTrees);
            _treeCollider = treePrefab.GetComponent<BoxCollider2D>();

            if(SerializationManager.Instance == null || SerializationManager.Instance.IsNewSave) SpawnInArea();
        }
        private void SpawnInArea()
        {
            var sizeX = _area.size.x;
            var transformPosition = _area.bounds.min;

            // calculate allowed trees
            var treeInArea = NumTreesAllowed(sizeX);
            _points = new List<Vector2>(treeInArea);
                
            CalculateTreePosition(transformPosition, sizeX);

            foreach (var tree in _points.Select(point => Instantiate(treePrefab, point, Quaternion.identity)))
            {
                _trees.Add(tree); // for saving and loading
            }
        }

        private Vector2 PlaceOnGround(float x, float fromY)
        {
            var results = new List<RaycastHit2D>();
            Physics2D.Raycast(new Vector2(x, fromY), Vector2.down, new ContactFilter2D().NoFilter(),results);
            var hit = results.Find(hit => hit.collider.CompareTag("Ground"));
            
            //Debug.DrawLine(point, hit.point, Color.red, 10, false);
            var newY = hit.point.y + _treeCollider.offset.y; // moving to ground position

            return new Vector2(x, newY);
        }

        private void CalculateTreePosition(Vector2 transformPosition, float sizeX)
        {
            var left = transformPosition.x;
            var right = left + sizeX;
            
            var treeOffsetX = _treeCollider.size.x / 2 + _treeCollider.offset.x;
            var newX = Random.Range(left + treeOffsetX, right - treeOffsetX); // trees can't spawn on the edges
            
            // Grounding
            var point = PlaceOnGround(newX, transformPosition.y);

            // Adding valid tree point
            if(_points.Capacity <= _points.Count + 1) return; // Max trees reached
            _points.Add(point);
            
            // Splitting the remaining area into 2 segments, recursively running to find more points
            var leftMax = newX - treeOffsetX;
            var leftSizeX = leftMax - transformPosition.x;
            if (TreeFits(leftSizeX))
            {
                CalculateTreePosition(transformPosition, leftSizeX);
            }
                
            var rightMin = newX + treeOffsetX;
            var rightSizeX = right - rightMin;
            if (TreeFits(rightSizeX))
            {
                CalculateTreePosition(new Vector2(rightMin, transformPosition.y), rightSizeX);
            }
        }

        private int NumTreesAllowed(float sizeX)
        {
            // number of colliders that would fit
            var maxTreeInArea = (int)Mathf.Round((int)(sizeX / _treeCollider.size.x) * density);
            // remaining space
            var maxCapacity = _trees.Capacity - _trees.Count;
            //Debug.Log((int)(sizeX / _treeCollider.size.x)+ ", max:" + maxTreeInArea);
            // lesser should dominate
            return Math.Min(maxCapacity, maxTreeInArea);
        }

        private bool TreeFits(float sizeX)
        {
            return _treeCollider.size.x <= sizeX;
        }

        public void SaveData(ref GameData data)
        {
            try
            {
                var savedPositions = _trees.Select(tree => tree.transform.position).ToList();
                // cleaning save data to get rid of non existing trees
                data.treePositions = savedPositions;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void LoadData(GameData data)
        {
            _trees ??= new List<GameObject>(); // making sure the list exists
            foreach (var treePosition in data.treePositions)
            {
                _trees.Add(Instantiate(treePrefab, treePosition, Quaternion.identity)); // for saving and loading
            }
        }
    }
}