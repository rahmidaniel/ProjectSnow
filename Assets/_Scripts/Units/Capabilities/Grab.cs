// using System.Linq;
// using _Scripts.Environment;
// using UnityEngine;
//
// namespace _Scripts.Units.Capabilities
// {
//     public class Grab : MonoBehaviour
//     {
//         public Transform holder;
//         
//         private TreeController _grabbedObject;
//
//         private ContactFilter2D _filter;
//         private bool _set;
//
//         private HingeJoint2D _joint2D;
//
//         public bool Grabbing { get; private set; }
//
//         private void Start()
//         {
//             _filter = new ContactFilter2D();
//             _filter.NoFilter();
//         }
//
//         private void Update()
//         {
//             if (!Input.GetKeyDown(KeyCode.G)) return;
//             
//             Grabbing = !Grabbing;
//             
//             if (Grabbing)
//             {
//                 if (GameManager.Instance.grabObjects.Count == 0) return;
//                 
//                 var target = GameManager.Instance.grabObjects.First();
//
//                 _grabbedObject = target;
//                 _set = true;
//
//                 // _joint2D = target.GetComponent<HingeJoint2D>();
//                 // if(_joint2D == null) _joint2D = target.AddComponent<HingeJoint2D>();
//                 //
//                 // _joint2D.connectedBody = GetComponent<Rigidbody2D>();
//                 // _joint2D.anchor = target.GetComponent<CapsuleCollider2D>().ClosestPoint(holder.position) 
//                 //                   - new Vector2(target.transform.position.x,);
//                 // _joint2D.connectedAnchor = holder.position;
//                 // _joint2D.enabled = true;
//
//                 // if (!set) return;
//
//                 _grabbedObject.transform.SetParent(holder, false);
//                 _grabbedObject.transform.SetPositionAndRotation(holder.position, Quaternion.identity);
//                 _grabbedObject.Rigidbody2D.isKinematic = true;
//                 _grabbedObject.Collider2D.enabled = false;
//
//             }
//             else
//             {
//                 if(!_set) return;
//
//                 _grabbedObject.transform.SetParent(null, false);
//                 _grabbedObject.Rigidbody2D.isKinematic = false;
//                 _grabbedObject.Collider2D.enabled = true;
//
//                 _grabbedObject = null;
//                 _set = false;
//             }
//         }
//     }
// }