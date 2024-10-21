/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using UnityEngine.UI;

// low-effort way to get a UI
public class HandMeshUI : MonoBehaviour
{
    public SphereCollider[] knobs;
    public TextMesh[] readouts;
    public Transform SphereTransform;

    int rightHeldKnob = -1;
    int leftHeldKnob = -1;

    public OVRSkeleton leftHand;
    public OVRSkeleton rightHand;

    public HandMeshMask leftMask;
    public HandMeshMask rightMask;
    public Material SphereMaterial;

    void Start()
    {
        //SetSliderValue(0, rightMask.radialDivisions, false);
        SetSliderValue(0, 0.0f, false);
        SetSliderValue(1, 0.0f, false);
        SetSliderValue(2, 0.0f, false);
        SetSliderValue(3, 0.0f, false);
        //SetSliderValue(1, rightMask.borderSize, false);
        //SetSliderValue(2, rightMask.fingerTaper, false);
        //SetSliderValue(3, rightMask.fingerTipLength, false);
        SetSliderValue(4, rightMask.webOffset, false);
    }

    void Update()
    {
        if (rightHand.IsDataValid)
        {
            Vector3 RfingerPos = rightHand.Bones[20].Transform.position;
            if (rightHeldKnob >= 0)
            {
                Vector3 localCursorPos = knobs[rightHeldKnob].transform.parent.InverseTransformPoint(RfingerPos);
                SetSliderValue(rightHeldKnob, Mathf.Clamp01(localCursorPos.x * 10), true);
                if (localCursorPos.z < -0.02f)
                {
                    rightHeldKnob = -1;
                }
            }
            else
            {
                for (int i = 0; i < knobs.Length; i++)
                {
                    if (Vector3.Distance(RfingerPos, knobs[i].transform.position) <= 0.02f && leftHeldKnob != i)
                    {
                        rightHeldKnob = i;
                        break;
                    }
                }
            }
        }

        if (leftHand.IsDataValid)
        {
            Vector3 LfingerPos = leftHand.Bones[20].Transform.position;
            if (leftHeldKnob >= 0)
            {
                Vector3 localCursorPos = knobs[leftHeldKnob].transform.parent.InverseTransformPoint(LfingerPos);
                SetSliderValue(leftHeldKnob, Mathf.Clamp01(localCursorPos.x * 10), true);
                if (localCursorPos.z < -0.02f)
                {
                    leftHeldKnob = -1;
                }
            }
            else
            {
                for (int i = 0; i < knobs.Length; i++)
                {
                    if (Vector3.Distance(LfingerPos, knobs[i].transform.position) <= 0.02f && rightHeldKnob != i)
                    {
                        leftHeldKnob = i;
                        break;
                    }
                }
            }
        }
    }

    void SetSliderValue(int sliderID, float value, bool isNormalized)
    {
        float sliderStart = 0.0f;
        float sliderEnd = 1.0f;
        float sliderScale = 0.1f;
        string displayString = "";
        switch (sliderID)
        {
            case 0:
                sliderStart = 0.0f;
                sliderEnd = 1.0f;
                displayString = "{0, 0:0.00}";
                break;
            case 1:
                sliderStart = 0.0f;
                sliderEnd = 1.0f;
                displayString = "{0, 0:0.00}";
                break;
            case 2:
                sliderStart = 0.0f;
                sliderEnd = 1.0f;
                displayString = "{0, 0:0.00}";
                break;
            case 3:
                sliderStart = -0.5f;
                sliderEnd = 0.5f;
                displayString = "{0, 0:0.00}";
                break;
            case 4:
                sliderStart = 0.0f;
                sliderEnd = 1.0f;
                displayString = "{0, 0:0.00}";
                break;
        }

        float absoluteValue = isNormalized ? value * (sliderEnd - sliderStart) + sliderStart : value;
        float normalizedValue = isNormalized ? value : (value - sliderStart) / (sliderEnd - sliderStart);
        knobs[sliderID].transform.localPosition = Vector3.right * normalizedValue * sliderScale;
        readouts[sliderID].text = string.Format(displayString, absoluteValue);

        // for both hands, set the properties
        switch (sliderID)
        {
            case 0:
                //rightMask.radialDivisions = (int)absoluteValue;
                //leftMask.radialDivisions = (int)absoluteValue;
                SphereMaterial.SetFloat("_SpherePercentajex", absoluteValue);
                SphereMaterial.SetFloat("_SpherePercentajey", absoluteValue);
                break;
            case 1:
                //SphereTransform.SetPositionAndRotation(new Vector3(SphereTransform.position.x, SphereTransform.position.y, absoluteValue*4), Quaternion.Euler(SphereTransform.rotation.eulerAngles.x, SphereTransform.rotation.eulerAngles.y, SphereTransform.rotation.eulerAngles.z));
                SphereTransform.localScale = new Vector3(SphereTransform.localScale.x,3.6f * absoluteValue, 3.6f * absoluteValue);
                //SphereMaterial.SetFloat("_SpherePercentajey", absoluteValue);
                break;
            case 2:
                SphereTransform.SetPositionAndRotation(new Vector3(SphereTransform.position.x, SphereTransform.position.y, SphereTransform.position.z), Quaternion.Euler(SphereTransform.rotation.eulerAngles.x, 360*absoluteValue, SphereTransform.rotation.eulerAngles.z));

                break;
            case 3:
                SphereTransform.SetPositionAndRotation(new Vector3(SphereTransform.position.x, SphereTransform.position.y, SphereTransform.position.z), Quaternion.Euler(SphereTransform.rotation.eulerAngles.x, SphereTransform.rotation.eulerAngles.y, 90 * (absoluteValue)));

                break;
            case 4:
                rightMask.webOffset = absoluteValue;
                leftMask.webOffset = absoluteValue;
                break;
        }
    }
}
