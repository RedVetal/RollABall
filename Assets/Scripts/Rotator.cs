using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(15, 30, 45);  // Это скорости вращения по осям X, Y, Z в градусах в секунду


    // Для визуальных эффектов (анимации, вращение, перемещение без физики) Update() — стандартный выбор.
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);   // Умножение на Time.deltaTime (время между кадрами) делает вращение независимым от FPS.
                                                               //  Без этого куб вращался бы быстрее на мощных ПК(с высоким FPS).
    }
}