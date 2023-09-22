using System;

public class ChangeSceneCollider : CustomComponent
{
    private bool activated = false;
    public float animationTime;
    private float time = 0.0f;
    public string nextScene;

    public void onInit()
    {
    }

    public void onUpdate(float deltaTime)
    {
        if (activated) {
            time += deltaTime;
            if (time > animationTime) {
                if (nextScene == "main")
                    InternalCalls.SetMouseHidden(false);
                InternalCalls.SetScene(nextScene);
                activated = false;
            }
        }
    }

    public void onCollisionEnter(ulong otherId)
    {
        Entity other = new Entity(otherId);

        if (other.hasComponent<SlimeControllable>()) {
            this.entity.log("Collision with SlimeControllable " + other.getName(), Toast.Warning);
            SlimeControllable slimeControllable = other.getComponent<SlimeControllable>();
            slimeControllable.speed = 0.0f;
            activated = true;
        }
    }
}
