using System;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;

namespace OpenCV4_System_Design
{
    public partial class Form1 : Form
    {
        // Net 객체 필드
        private Net NET;
        // 비디오 캡처 객체 필드
        private VideoCapture CAPTURE;
        // 프레임 처리 상태 플래그
        private bool _isRunning;
        // Tensorflow 모델 파일 경로
        private string PATH_MODEL = "C:\\JHIWHOON_ws\\_Tech University of Korea\\LAB\\_2023 시스템설계 교과목 개발\\_강의자료\\개정 강의자료\\최종\\OpenCV\\tensorflow\\tensorflow_model\\frozen_inference_graph.pb";
        // Tensorflow 구성 파일 경로
        private string PATH_CONFIG = "C:\\JHIWHOON_ws\\_Tech University of Korea\\LAB\\_2023 시스템설계 교과목 개발\\_강의자료\\개정 강의자료\\최종\\OpenCV\\tensorflow\\tensorflow_model\\graph.pbtxt";
        // 클래스 이름을 저장하는 배열
        private string[] CLASS_NAME = File.ReadAllLines("labelmap.txt");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TensorFlow 모델과 구성 파일을 사용하여 신경망을 로드
            NET = CvDnn.ReadNetFromTensorflow(PATH_MODEL, PATH_CONFIG);

            // 1번 카메라를 사용하여 VideoCapture 객체를 초기화
            CAPTURE = new VideoCapture(1);

            // 카메라가 열리지 않았을 경우 경고 메시지를 표시하고 메서드를 종료
            if (!CAPTURE.IsOpened())
            {
                MessageBox.Show("카메라를 열 수 없습니다.");
                return;
            }

            // 프레임 처리를 시작하는 플래그를 true로 설정
            _isRunning = true;

            // 별도의 스레드를 생성하여 ProcessFrame 메서드를 실행
            var thread = new System.Threading.Thread(ProcessFrame);
            thread.Start();
        }


        private void ProcessFrame()
        {
            // _isRunning이 true인 동안 루프를 계속 실행
            while (_isRunning)
            {
                // Mat 객체를 사용하여 프레임을 저장
                using (var frame = new Mat())
                {
                    // 비디오 캡처 객체에서 프레임 읽기
                    CAPTURE.Read(frame);

                    // 프레임이 비어 있는 경우 다음 루프로 넘어감
                    if (frame.Empty())
                    {
                        continue;
                    }                        

                    // 이미지에서 블롭(blob) 생성
                    var blob = CvDnn.BlobFromImage(frame, 1.0, new OpenCvSharp.Size(300, 300), new OpenCvSharp.Scalar(0, 0, 0), true, false);

                    // 신경망의 입력으로 블롭 설정
                    NET.SetInput(blob);

                    // 신경망을 통해 추론을 수행하고 결과를 출력
                    var output = NET.Forward();

                    // 감지된 객체를 처리
                    ProcessDetections(frame, output);

                    // Mat 객체의 복사본을 생성하여 BeginInvoke에서 사용
                    var frameCopy = frame.Clone();

                    // UI 스레드에서 PictureBox에 이미지를 설정
                    BeginInvoke(new Action(() =>
                    {
                        // 기존의 이미지를 삭제하여 메모리 누수 방지
                        PB_ORIGINAL.Image?.Dispose();

                        // PictureBox에 새로운 이미지 설정
                        PB_ORIGINAL.Image = BitmapConverter.ToBitmap(frameCopy);

                        // 복사본을 사용한 후에 Dispose 호출
                        frameCopy.Dispose();
                    }));
                }
            }
        }


        private void ProcessDetections(Mat frame, Mat output)
        {
            // 신뢰도 임계값 설정
            float threshold = 0.5f;

            // 출력 결과의 각 감지 항목을 반복 처리
            for (int i = 0; i < output.Size(2); i++)
            {
                // 감지 항목의 신뢰도 가져오기
                float confidence = output.At<float>(0, 0, i, 2);

                // 신뢰도가 임계값을 초과하는 경우에만 처리
                if (confidence > threshold)
                {
                    // 감지된 객체의 클래스 ID 가져오기
                    int classId = (int)output.At<float>(0, 0, i, 1);

                    // 경계 상자의 좌표 계산
                    int left = (int)(output.At<float>(0, 0, i, 3) * frame.Cols);
                    int top = (int)(output.At<float>(0, 0, i, 4) * frame.Rows);
                    int right = (int)(output.At<float>(0, 0, i, 5) * frame.Cols);
                    int bottom = (int)(output.At<float>(0, 0, i, 6) * frame.Rows);

                    // 프레임에 경계 상자 그리기
                    Cv2.Rectangle(frame, new OpenCvSharp.Point(left, top), new OpenCvSharp.Point(right, bottom), Scalar.Red, 2);

                    // 클래스 이름과 신뢰도 레이블 생성
                    string label = $"{CLASS_NAME[classId]}: {confidence * 100:0.00}%";

                    // 프레임에 레이블 그리기
                    Cv2.PutText(frame, label, new OpenCvSharp.Point(left, top - 10), HersheyFonts.HersheySimplex, 0.5, Scalar.Red, 2);
                }
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프레임 처리 중지
            _isRunning = false;
            // 장치 해제
            CAPTURE.Release();
        }
    }
}