# 한국공학대학교 시스템 설계 교과목 강의 참고자료입니다.
다운로드를 통해 실습에서 사용될 예제 코드를 사용해보실 수 있습니다.

해당 실습은 특히, 머신러닝을 사용할 경우 컴퓨터 환경에 따라 실행이 잘 되지 않을 수 있습니다.
`Window` 및 `NVIDIA 그래픽 카드`를 사용할 것을 권장합니다.

필자 개발 환경1
|구성 요소|상세 스펙(Spec)|
|---|---|
|운영체제(OS)|**Windows11**|
|그래픽 카드(GPU)|**NVIDIA GeForce RTX 3070**|
|카메라|**Intel® RealSense™ depth camera D435i**|

필자 개발 환경2
|구성 요소|상세 스펙(Spec)|
|---|---|
|운영체제(OS)|**Windows10**|
|그래픽 카드(GPU)|**NVIDIA GeForce RTX 3070**|
|카메라|**Intel® RealSense™ depth camera D435i**|


사용 중 오류가 발생할 경우 kangjw0914@naver.com 또는 1208cksals@naver.com 그리고 Issues를 활용해주시길 바랍니다.

## 목차
[강의 참고자료 관련](#강의-참고자료-관련)  
[1. 강의 참고자료 사용 방법](#강의-참고자료-사용-방법)  
[2. C#](#csharp)  
[3. OpenCV](#opencv)  
[4. Stage UI](#stage-ui)  


## 강의 참고자료 관련
<div align="right">  
  
[목차로](#목차)

</div>

### 강의 참고자료 사용 방법
1. 우측 상단(About 옆)의 ***<> Code***를 클릭
2. ***Download ZIP*** 클릭
<div align="right">  
  
[목차로](#목차)

</div>

### 강의 참고 자료 상세 설명
#### CSharp

#### OpenCV
개발 환경
|구성 요소|상세 스펙(Spec)|
|---|---|
|운영체제(OS)|**Windows11**|
|IDE|**Visual Studio 2022**|
|Framework|**Windows Forms**|
|Nuget|**OpenCvSharp<br>OpenCvSharp.Extensions<br>OpenCvSharp.runtime.win**|

- 파일명은 강의자료 000코드 실습에서 000 입니다.
- 리소스에 이미지가 없을 경우 gibhub>image에 들어가면 강의에 사용했떤 모든 이미지가 들어가 있습니다. 이미지명을 확인하고 추가하면 됩니다.
- 프로그램 실행은 실습 폴더를 들어간 뒤 ***OpenCV4-System-Design.sln***을 실행하면 됩니다.
<div align="right">  
  
[목차로](#목차)

</div>

#### Stage UI
<div align="right">  
  
[목차로](#목차)

</div>

## WSL이 필요한 경우
*러닝이 된 모델을 다운로드 받을 때 Linux를 요구하는 경우가 존재<br>이를 위해 WSL을 활용하는 방법 소개*

1. 관리자 권한으로 powershell 실행
2. 아래 명령어 입력
```bash
wsl --install
```
```bash
  C:\Windows\System32>wsl --install
    설치 중: 가상 머신 플랫폼
    가상 머신 플랫폼이(가) 설치되었습니다.
    설치 중: Linux용 Windows 하위 시스템
    Linux용 Windows 하위 시스템이(가) 설치되었습니다.
    설치 중: Ubuntu
    Ubuntu이(가) 설치되었습니다.
    요청한 작업이 잘 실행되었습니다. 시스템을 다시 시작하면 변경 사항이 적용됩니다.
```
3. 시스템 재시작
4. 아래 명령어를 통해 사용 가능한 배포판 목록 확인 
```bash
wsl --list --online
```
```bash
PS C:\WINDOWS\system32> wsl --list --online
    다음은 설치할 수 있는 유효한 배포판 목록입니다.
    'wsl.exe --install <Distro>'를 사용하여 설치합니다.
    
    NAME                                   FRIENDLY NAME
    Ubuntu                                 Ubuntu
    Debian                                 Debian GNU/Linux
    kali-linux                             Kali Linux Rolling
    Ubuntu-18.04                           Ubuntu 18.04 LTS
    Ubuntu-20.04                           Ubuntu 20.04 LTS
    Ubuntu-22.04                           Ubuntu 22.04 LTS
    Ubuntu-24.04                           Ubuntu 24.04 LTS
    OracleLinux_7_9                        Oracle Linux 7.9
    OracleLinux_8_7                        Oracle Linux 8.7
    OracleLinux_9_1                        Oracle Linux 9.1
    openSUSE-Leap-15.6                     openSUSE Leap 15.6
    SUSE-Linux-Enterprise-15-SP5           SUSE Linux Enterprise 15 SP5
    SUSE-Linux-Enterprise-Server-15-SP6    SUSE Linux Enterprise Server 15 SP6
    openSUSE-Tumbleweed                    openSUSE Tumbleweed
```
5-1. MS Store에서 원하는 버전의 ubuntu 설치
필자: ***Ubuntu 20.04.6 LTS*** 설치
유효한 배포판일 경우 다른 버전도 가능

5-2. 또는 명령어를 통해 설치 가능
```bash
wsl --install -d Ubuntu-20.04
```
```bash
PS C:\WINDOWS\system32> wsl --install -d Ubuntu-20.04
    Ubuntu 20.04 LTS이(가) 이미 설치되어 있습니다.
    Ubuntu 20.04 LTS을(를) 시작하는 중...
    To run a command as administrator (user "root"), use "sudo <command>".
    See "man sudo_root" for details.
```
6. 위에서 설치한 ubuntu 실행 및 username, password 설정
```bash
Installing, this may take a few minutes...
Please create a default UNIX user account. The username does not need to match your Windows username.
For more information visit: https://aka.ms/wslusers
Enter new UNIX username: jhiwhoon
New password:
Retype new password:
passwd: password updated successfully
Installation successful!
```
7. 관리자 권한으로 Windows PowerShell 실행
8. 기본 WSL 버전을 WSL2로 설정
```bash
    wsl --set-default-version 2
```
```bash
    wsl --set-default-version 2
    PS C:\WINDOWS\system32> wsl --set-default-version 2
    >>
    WSL 2와의 주요 차이점에 대한 자세한 내용은 https://aka.ms/wsl2를 참조하세요
    
    작업을 완료했습니다.
```
참고: 기본적으로 `wsl --install` 명령을 사용하여 설치된 새 Linux 설치는 기본적으로 WSL 2로 설정됨

        
9. 시작 메뉴에서 `WSL` 검색하여 접속하면
```bash
    To run a command as administrator (user "root"), use "sudo <command>".
    See "man sudo_root" for details.
    
    jhiwhoon@JhiWhoon:~$
```
    정상적으로 설치됨을 확인 가능

*추가적인 정상적인 설치 확인 방법
윈도우 파워쉘
```bash
wsl --list --verbose
```
```bash
PS C:\WINDOWS\system32> wsl --list --verbose
>>
  NAME            STATE           VERSION
* Ubuntu-20.04    Running         2
```
WSL
```bash
lsb_release -a
```
```bash
jhiwhoon@JhiWhoon:~$ lsb_release -a
No LSB modules are available.
Distributor ID: Ubuntu
Description:    Ubuntu 20.04.6 LTS
Release:        20.04
Codename:       focal
```

**조건: Windows 10 버전 2004 이상(빌드 19041 이상) 또는 Windows 11**
*해당 조건을 충족하지 못하는 경우*
*https://learn.microsoft.com/ko-kr/windows/wsl/install-manual*

### 모델 불러오는 과정 예시
*학습된 모델 불러오는 것을 권장*
**ssd_mobilenet_v1_coco 모델을 다운로드하고 사용하는 예제**

1. 원하는 모델 사이트 접속
```bash
https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf1_detection_zoo.md
```
필자는 위 사이트에서 다운로드한 상황

2. 원하는 모델 다운로드 링크 확인
```bash
http://download.tensorflow.org/models/object_detection/ssd_mobilenet_v1_coco_2018_01_28.tar.gz
```
3. WSL을 실행하여 아래 명령어 입력(위 WSL이 준비되어 있다는 가정)
```bash
wget http://download.tensorflow.org/models/object_detection/ssd_mobilenet_v1_coco_2018_01_28.tar.gz
```
```bash
jhiwhoon@JhiWhoon:~$ wget http://download.tensorflow.org/models/object_detection/ssd_mobilenet_v1_coco_2018_01_28.tar.gz
--2024-08-04 18:05:01--  http://download.tensorflow.org/models/object_detection/ssd_mobilenet_v1_coco_2018_01_28.tar.gz
Resolving download.tensorflow.org (download.tensorflow.org)... 34.64.4.27, 34.64.4.91, 34.64.4.59, ...
Connecting to download.tensorflow.org (download.tensorflow.org)|34.64.4.27|:80... connected.
HTTP request sent, awaiting response... 200 OK
Length: 76541073 (73M) [application/x-tar]
Saving to: ‘ssd_mobilenet_v1_coco_2018_01_28.tar.gz’

ssd_mobilenet_v1_coco_2018_01 100%[=================================================>]  73.00M  10.6MB/s    in 7.4s

2024-08-04 18:05:09 (9.84 MB/s) - ‘ssd_mobilenet_v1_coco_2018_01_28.tar.gz’ saved [76541073/76541073]
```
4. 압축 해제를 위해 아래 명령어 입력
```bash
tar -xzvf ssd_mobilenet_v1_coco_2018_01_28.tar.gz
```
```bash
jhiwhoon@JhiWhoon:~$ tar -xzvf ssd_mobilenet_v1_coco_2018_01_28.tar.gz
ssd_mobilenet_v1_coco_2018_01_28/
ssd_mobilenet_v1_coco_2018_01_28/model.ckpt.index
ssd_mobilenet_v1_coco_2018_01_28/checkpoint
ssd_mobilenet_v1_coco_2018_01_28/pipeline.config
ssd_mobilenet_v1_coco_2018_01_28/model.ckpt.data-00000-of-00001
ssd_mobilenet_v1_coco_2018_01_28/model.ckpt.meta
ssd_mobilenet_v1_coco_2018_01_28/saved_model/
ssd_mobilenet_v1_coco_2018_01_28/saved_model/saved_model.pb
ssd_mobilenet_v1_coco_2018_01_28/saved_model/variables/
ssd_mobilenet_v1_coco_2018_01_28/frozen_inference_graph.pb
````
6. 디렉토리 확인
```bash
ls
```
```bash
jhiwhoon@JhiWhoon:~$ ls
ssd_mobilenet_v1_coco_2018_01_28  ssd_mobilenet_v1_coco_2018_01_28.tar.gz
```
7. cd 명령어 활용 압축 해제한 디렉토리로 이동
*cd(Change Directory): 디렉토리 변경 명령어*
```bash
cd ~/<경로>
```
```bash
jhiwhoon@JhiWhoon:~$ cd ~/ssd_mobilenet_v1_coco_2018_01_28/
```
9. 디렉토리 확인
```bash
ls
```
```bash
jhiwhoon@JhiWhoon:~/ssd_mobilenet_v1_coco_2018_01_28$ ls
checkpoint                 model.ckpt.data-00000-of-00001  model.ckpt.meta  saved_model
frozen_inference_graph.pb  model.ckpt.index                pipeline.config
```
10. 파일 탐색기 탐색
```bash
explorer.exe .
```
```bash
jhiwhoon@JhiWhoon:~/ssd_mobilenet_v1_coco_2018_01_28$ explorer.exe .
```

***추가적인 기능***
*특정 확장자 검색*
```bash
find ~ -name "*<확장자>"
```
```bash
jhiwhoon@JhiWhoon:~$ find ~ -name "*.pb"
/home/jhiwhoon/ssd_mobilenet_v1_coco_2017_11_17/frozen_inference_graph.pb
/home/jhiwhoon/ssd_mobilenet_v1_coco_2017_11_17/saved_model/saved_model.pb
```

<div align="right">  
  
[목차로](#목차)

</div>

