using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Transform pemain yang akan diikuti oleh kamera
    public float smoothSpeed = 5.0f; // Kecepatan pergerakan kamera

    void LateUpdate()
    {
        if (target != null)
        {
            // Menghitung posisi yang diinginkan untuk kamera
            Vector3 desiredPosition = target.position + new Vector3(0, 0, -10); // -10 adalah jarak kamera dari objek pemain

            // Menggunakan Lerp untuk menghaluskan pergerakan kamera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Memperbarui posisi kamera
            transform.position = smoothedPosition;
        }
    }
}
