using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HandTrackingScript : MonoBehaviour
{
    public Camera sceneCamera;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public OVRSkeleton skeleton;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
    private bool isIndexFingerPinching;

    private LineRenderer line;
    private Transform p0;
    private Transform p1;
    private Transform p2;

    private Transform handIndexTipTransform;



    void Start()
    {

        // 将Cube的位置设置为正前方1m处
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 1.0f;

        // 获得LineRenderer组件
        line = GetComponent<LineRenderer>();

    }

    void Update()
    {

        //每帧的步长
        step = 5.0f * Time.deltaTime;

        // 判断手部追踪是否启用
        if (leftHand.IsTracked)
        {
            // 通过GetFingerIsPinching函数判断左手是否做出了捏合动作
            isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            // Proceed only if left hand is pinching
            if (isIndexFingerPinching)
            {
                // 启用LineRenderer
                line.enabled = true;

                // 将Cube位置变换到某个地方
                pinchCube();

                // 对手部的骨骼进行遍历，得到食指指尖骨骼的transform
                foreach (var b in skeleton.Bones)
                {
                    // If bone is the the hand index tip
                    if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                    {
                        // Store its transform and break the loop
                        handIndexTipTransform = b.Transform;
                        break;
                    }
                }

                // p0是Cube的transform，p2是左手食指指尖的transform
                // 将这个两个位置作为线条的两个端点
                p0 = transform;
                p2 = handIndexTipTransform;


                // p1位置在中心摄像机正前方0.8m处的位置
                p1 = sceneCamera.transform;
                p1.position += sceneCamera.transform.forward * 0.8f;

                // 绘制左手食指端点到Cube的曲线
                DrawCurve(p0.position, p1.position, p2.position);
            }
            // 如果没有做捏合动作
            else
            {
                //禁用LineRenderer
                line.enabled = false;
            }
        }

    }
    //根据三个点绘制曲线
    public void DrawCurve(Vector3 point_0, Vector3 point_1, Vector3 point_2)
    {

        // Set the number of segments to 200
        line.positionCount = 200;
        Vector3 B = new Vector3(0, 0, 0);
        float t = 0f;

        // Draw segments
        for (int i = 0; i < line.positionCount; i++)
        {
            // Move to next segment
            t += 0.005f;

            B = (1 - t) * (1 - t) * point_0 + 2 * (1 - t) * t * point_1 + t * t * point_2;
            line.SetPosition(i, B);
        }
    }

    void pinchCube()
    //平滑的移动和旋转Cube到左手附近的某个位置
    {
        targetPosition = leftHand.transform.position - leftHand.transform.forward * 0.4f;
        targetRotation = Quaternion.LookRotation(transform.position - leftHand.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
}
