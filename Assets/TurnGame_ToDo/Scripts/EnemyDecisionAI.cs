using System.Collections.Generic;
using UnityEngine;
using TurnGame;
namespace TurnGame.Student
{
    public class EnemyDecisionAI : IEnemyDecisionAI
    {
        public int SelectTarget(IReadOnlyList<BattleSnapshot> units, BattleElement Attackelement)
        {
            int best = -1;
            float bestScore = float.MinValue;

            for (int i = 0; i < units.Count; i++)
            {
                // TODO 27: 죽은 캐릭터와 적 팀은 제외하세요.
                // TODO 28: HP가 낮을수록 최대 40점을 더하세요.
                // TODO 29: 이번 공격으로 쓰러뜨릴 수 있으면 100점을 더하세요.
                // TODO 30: 속성 약점이면 30점을 더하세요.
                // TODO 31: 가장 높은 점수의 인덱스를 저장하세요.

                BattleSnapshot target = units[i];

                if (!target.IsAlive || target.Team != BattleTeam.Heroes) continue;
                float hpRotio = target.HP / (float)Mathf.Max(1, target.MaxHP);
                float score = (1f - hpRotio) * 40F;

                if(target.HP <= 25)
                {
                    score += 100f;
                }

                if(Attackelement != BattleElement.None && target.Weakness == Attackelement)
                {
                    score += 30f;
                }

                if(target.Guarding)
                {
                    score -= 15f;
                }

                if(score > bestScore)
                {
                    bestScore = score;
                    best = i;
                }
            }
            return best;
        }
    }
}
