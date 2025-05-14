```md
📌 펫 팔로우 시스템 

`PetFollow`는 플레이어를 따라다니는 펫의 움직임과, 일정 시간마다 아이템을 전달하는 기능을 담당합니다.  
펫은 `Idle`, `GoingToGive`, `Returning` 세 가지 상태를 가지며, 각 상태에 따라 다른 행동을 수행합니다.

---

## 주요 변수

- `player` : 따라다닐 대상인 플레이어의 Transform  
- `followOffset` : 플레이어 기준 펫의 위치 오프셋
- `followSpeed` : 플레이어를 따라다닐 때 속도  
- `itemData` : 생성할 아이템 정보 
- `ItemGiveInterval` : 아이템을 주는 시간 간격 
- `givingItemSpeed` : 아이템을 주고 돌아오는 동안의 이동 속도
- `giveItemPosition` : 아이템을 주는 목표 위치  

---

## 상태 Enum

```csharp
private enum PetState { Idle, GoingToGive, Returning }
```

- `Idle` : 플레이어 곁에서 대기 중  
- `GoingToGive` : 아이템을 전달하러 이동 중  
- `Returning` : 전달 후 다시 플레이어에게 돌아오는 중  

---

## 메서드 설명

### Start()

- `InvokeRepeating()`을 이용해 일정 시간 간격으로 `HandleItemPosition()`을 호출하여 아이템 지급급

---

### Update()

- 현재 펫의 상태에 따라 행동 로직 수행  
  - `Idle` 상태: 플레이어의 옆 위치로 이동  
  - `GoingToGive`: 아이템 전달 위치로 이동, 도착 시 `SpawnItem()` 호출 후 `Returning` 상태로 변경  
  - `Returning`: 다시 플레이어 옆으로 이동, 도착 시 `Idle` 상태로 변경  

---

### HandleItemPosition()

- 펫이 아이템을 주러 가기 위한 위치를 설정  
- 상태를 `GoingToGive`로 변경하여 다음 프레임에서 이동 시작  

---

### SpawnItem()

- `itemData.Prefab`을 기반으로 아이템 오브젝트 생성  
- 생성된 오브젝트에 `itemData`를 설정하여 해당 아이템이 어떤 것인지 지정  

```
