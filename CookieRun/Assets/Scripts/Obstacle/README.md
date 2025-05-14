<div align="center">
# 4rd_Week_Sparta

# 와이어프레임
![Cookie_Run_WireFrame](https://github.com/user-attachments/assets/386db7b9-8766-4f43-86bf-fc9d5f2bbc2c)


# GitHub Commit Convention
add: 파일 추가

feat: 새로운 기능 추가

fix: 버그 수정

style: 코드 스타일 수정(정렬, 들여쓰기)

Update: 텍스트 수정 (소개문구, 출력문구 등)

Delete : 파일 삭제
</div>


Obstacle - 담당자 송민권

ObstacleManager.cs

Awake
싱글톤

Start
초기 장애물(발판) 생성

LoadObstacles
장애물 프리팹 리스트 저장

CreateObstacle
장애물 생성 위치 지정 밑 생성


Obstacle.cs

Awake
각종 컴포넌트 호출

OnTriggerEnter2D
플레이어와 충돌 확인

BreakAndFly
파괴 파티클 생성

FlyAndDestroy
파괴 애니메이션 생성 밑 장애물 파괴