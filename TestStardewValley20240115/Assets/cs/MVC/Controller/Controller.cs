using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected void Bind(View view,Model model)
    {
        model.AddUpdateEvent(view.UpdateView);
    }
}
