using System;

public class KillingCollider : CustomComponent
{
    private bool activated = false;
    private float time = 0.0f;
    private Entity slime;

    private static float easeOutCubic(float x) {
        return 1 - MathF.Pow(1 - x, 3);
    }

    public void onInit()
    {
    }

    public void onUpdate(float deltaTime)
    {
        if (activated) {
            time += deltaTime;

            float alpha = 1 - easeOutCubic(time);

            Vector3 scale = slime.getComponent<Transform>().scale;
            scale.x = alpha;
            scale.y = alpha;
            scale.z = alpha;
            slime.getComponent<Transform>().scale = scale;
            if (time > 1.0f) {
                InternalCalls.ResetScene();
                activated = false;
            }
        }
    }

    public void onCollisionEnter(ulong otherId)
    {
        Entity other = new Entity(otherId);

        if (other.hasComponent<SlimeControllable>()) {
            this.entity.log("You died...", Toast.Error);
            SlimeControllable controllable = other.getComponent<SlimeControllable>();
            controllable.speed = 0.0f;
            activated = true;
            slime = other;
        }
    }
}
