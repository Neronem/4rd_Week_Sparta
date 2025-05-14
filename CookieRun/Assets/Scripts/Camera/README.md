# 📌 카메라(Camera)

# BgLooper.cs

배경과 장애물의 순환을 담당합니다

Start
장애물 호출

OnTriggerEnter2D
장애물 충돌 확인
장애물 생성 위치 지정

OnTriggerExit2D
오브젝트 충돌 확인
배경 이동 위치 지정 밑 이동
장애물 파괴



# CameraShake.cs

카메라 진동 효과를 담당합니다

Shake
카메라 진동 효과 재생



# FollowCamera.cs

카메라 이동을 담당합니다

Start
타겟 지정 밑 초기 위치 지정

LateUpdate
타겟 추적 이동