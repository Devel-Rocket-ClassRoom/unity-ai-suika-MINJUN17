# CLAUDE.md — unity-ai-suika-MINJUN17

## 프로젝트 개요

Unity 2D로 구현하는 수박게임(Suika Game) 클론.
**GDD 기준문서**: `Docs/GDD.md` — 모든 구현 판단은 이 문서를 참고한다.

## 프로젝트 구조

```
unity-ai-suika-MINJUN17/
├── watermelon/          # Unity 프로젝트 루트
│   └── Assets/
│       ├── Scenes/      # SampleScene.unity (메인 씬)
│       └── Scripts/     # C# 스크립트
├── Docs/
│   └── GDD.md           # Game Design Document
└── CLAUDE.md
```

## Unity MCP 작업 규칙

- 씬 오브젝트 생성/수정은 `Unity_RunCommand` 사용
- 스크립트 파일은 `watermelon/Assets/Scripts/`에 작성
- 스크립트 추가 후 Unity 컴파일 완료 확인 후 RunCommand로 씬에 부착
- 씬 확인 시 `Unity_Camera_Capture` 사용

## 코드 규칙

- 스크립트 한 파일에 클래스 하나
- MonoBehaviour는 `[RequireComponent]`로 의존성 명시
- `Fruit.cs` — 과일 데이터 + 충돌 머지 로직
- `GameManager.cs` — 싱글톤, 드롭/점수/게임오버 관리
- `DeathZone.cs` — 게임오버 트리거

## 과일 레벨 기준 (GDD 요약)

Lv0 체리 → Lv1 딸기 → Lv2 포도 → Lv3 귤 → Lv4 감 →
Lv5 사과 → Lv6 배 → Lv7 복숭아 → Lv8 파인애플 → Lv9 멜론 → Lv10 수박

드롭 가능: Lv0~4만 랜덤 생성
