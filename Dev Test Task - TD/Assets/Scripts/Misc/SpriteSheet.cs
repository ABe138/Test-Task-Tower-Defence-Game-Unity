using UnityEngine;

//4-directional sprite sheet
[System.Serializable]
public class SpriteSheet {

    [SerializeField]
    private Sprite left;
    [SerializeField]
    private Sprite right;
    [SerializeField]
    private Sprite front;
    [SerializeField]
    private Sprite back;

    public Sprite GetSprite(Vector2 direction)
    {
        bool horizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? true : false;
        if(horizontal)
        {
            if (direction.x > 0) return right;
            else return left;
        }
        else
        {
            if (direction.y > 0) return back;
        }
        return front;
    }

}
