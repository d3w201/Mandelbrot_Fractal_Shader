using UnityEngine;

public class Explorer : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Vector2 pos;
    [SerializeField] private float scale,angle;

    private Vector2 _smoothPos;
    private float _smoothScale, _smoothAngle;

    private void UpdateShader()
    {
        _smoothPos = Vector2.Lerp(_smoothPos, pos, .03f);
        _smoothScale = Mathf.Lerp(_smoothScale, scale, .03f);
        _smoothAngle = Mathf.Lerp(_smoothAngle, angle, .03f);

        var aspect = (float)Screen.width / (float)Screen.height;

        var scaleX = _smoothScale;
        var scaleY = _smoothScale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;
        
        mat.SetVector("_Area",new Vector4(_smoothPos.x,_smoothPos.y,scaleX,scaleY));
        mat.SetFloat("_Angle",_smoothAngle);
    }
    
    void Update()
    {
        UpdateShader();
        HandleInputs();
    }

    private void HandleInputs()
    {
        //zoom
        if (Input.GetKey(KeyCode.KeypadPlus))
            scale *= .99f;
        if (Input.GetKey(KeyCode.KeypadMinus))
            scale += 1.01f;
        
        //rotation
        if (Input.GetKey(KeyCode.E))
            angle -= .01f;
        if (Input.GetKey(KeyCode.Q))
            angle += .01f;

        Vector2 dir = new Vector2(.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c, dir.x * s);
        
        //positionX
        if (Input.GetKey(KeyCode.A))
            pos -= dir;
        if (Input.GetKey(KeyCode.D))
            pos += dir;

        dir = new Vector2(-dir.y, dir.x);
        
        //positionY
        if (Input.GetKey(KeyCode.S))
            pos -= dir;
        if (Input.GetKey(KeyCode.W))
            pos += dir;
    }
}
