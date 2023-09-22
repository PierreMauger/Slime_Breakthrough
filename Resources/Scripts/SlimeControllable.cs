using System;

public class SlimeControllable : CustomComponent
{
    private Transform transform;
    private Camera camera;
    public Entity cameraEntity = null;
    private float time = 0.0f;
    public float speed = 10.0f;
    private Vector3 moveDirection;

    private static float DeltaBetweenAngle(float current, float target)
    {
        return (target - current + 540.0f) % 360.0f - 180.0f;
    }

    private static float LerpBetweenAngle(float current, float target, float t)
    {
        return current + t * DeltaBetweenAngle(current, target);
    }

    public void onInit()
    {
        transform = this.entity.getComponent<Transform>();
        if (cameraEntity == null) {
            Console.WriteLine("No camera entity specified, using default camera (1).");
            cameraEntity = new Entity(1);
        }
        camera = cameraEntity.getComponent<Camera>();
    }

    public void onUpdate(float deltaTime)
    {
        time += deltaTime;

        Vector3 tmpCamForward = camera.forward;
        float tmpCamForwardLength = MathF.Sqrt(tmpCamForward.x * tmpCamForward.x + tmpCamForward.z * tmpCamForward.z);
        tmpCamForward.x /= tmpCamForwardLength;
        tmpCamForward.z /= tmpCamForwardLength;

        Vector3 tmpPos = transform.position;
        moveDirection = new Vector3();

        if (InternalCalls.IsKeyDown(0x57)) { // W
            moveDirection += tmpCamForward;
        }
        if (InternalCalls.IsKeyDown(0x41)) { // A
            moveDirection += new Vector3(tmpCamForward.z, 0, -tmpCamForward.x);
        }
        if (InternalCalls.IsKeyDown(0x53)) { // S
            moveDirection -= tmpCamForward;
        }
        if (InternalCalls.IsKeyDown(0x44)) { // D
            moveDirection += new Vector3(-tmpCamForward.z, 0, tmpCamForward.x);
        }

        if (moveDirection != new Vector3() && speed != 0.0f) {
            moveDirection.y = 0.0f;
            moveDirection = moveDirection.normalize();
            tmpPos += moveDirection * speed * (MathF.Sin(time * 10.0f) * 0.5f + 1.0f) * deltaTime;
            transform.position = tmpPos;

            // animate with scale
            Vector3 tmpScale = transform.scale;
            tmpScale.y = MathF.Sin(time * 10.0f) * 0.1f + 1.0f;
            tmpScale.x = MathF.Cos(time * 10.0f) * 0.1f + 1.0f;
            tmpScale.z = MathF.Cos(time * 10.0f) * 0.1f + 1.0f;
            transform.scale = tmpScale;

            // rotate to camera smoothly
            Vector3 tmpRot = transform.rotation;
            tmpRot.y = (tmpRot.y + 360.0f) % 360.0f;
            float newRotY = -MathF.Atan2(moveDirection.z, moveDirection.x) * 180.0f / MathF.PI;
            tmpRot.y = LerpBetweenAngle(tmpRot.y, newRotY, deltaTime * 10.0f);
            transform.rotation = tmpRot;
        } else {
            // smooth scale to 1.0
            Vector3 tmpScale = transform.scale;
            tmpScale.y += (1.0f - tmpScale.y) * deltaTime * 10.0f;
            tmpScale.x += (1.0f - tmpScale.x) * deltaTime * 5.0f;
            tmpScale.z += (1.0f - tmpScale.z) * deltaTime * 5.0f;
            transform.scale = tmpScale;
        }
    }
}
