using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] Transform _path;
    [SerializeField] float _speed;
    [SerializeField] bool _canFlipPath;

    private Transform[] _points;
    private int _currentPoint = 0;

    private void Start() {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++) {
            _points[i] = _path.GetChild(i);
        }

        transform.position = _points[0].position;
    }

    private void Update() {
        Transform target = _points[_currentPoint];
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position) {
            _currentPoint++;

            if (_currentPoint >= _points.Length) {
                if (_canFlipPath) {
                    ArrayFlip(_points);
                }
                
                _currentPoint = 0;
            }
        }
    }

    private void ArrayFlip(Transform[] array) {
        for (int i = 1; i <= array.Length - i; i++) {
            Transform buffer = array[i - 1];

            array[i - 1] = array[array.Length - i];
            array[array.Length - i] = buffer;
        }
    }
}