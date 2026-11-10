using System;
using System.Collections.Generic;
using AlgoLesson8;
using Unity.VisualScripting.Antlr3.Runtime;

namespace AlgoLesson8.Student
{
    /// <summary>
    /// 긴급도, 남은 시간, 보상 순서로 다음 주문을 선택합니다.
    /// </summary>
    public sealed class OrderScheduler : IOrderScheduler
    {
        public int SelectNextOrder(IReadOnlyList<WorkOrder> orders,float currentTime)
        {
            if (orders == null || orders.Count == 0)                     //처리할 주문이 없으면 -1
                return -1;

            //첫번째 주문을 임시 최선 후보로 두고, 나머지와 하나씩 비교한다.
            WorkOrder best = orders[0];

            for(int i = 1; i<orders.Count; i++)
            {
                WorkOrder candidate = orders[i];

                //candidate 가 best보다 더 좋은 주문인지 판정
                bool better =
                    candidate.Priority > best.Priority ||                                                       //우선순위가 더 높다
                    (candidate.Priority == best.Priority && candidate.Deadline < best.Deadline) ||              //우선순위가 같고, 마감이 더 빠르다
                    (candidate.Priority == best.Priority && candidate.Deadline == best.Deadline && candidate.Reward > best.Reward) ||        //3. 우선순위 마감이 같고 보상이 더 크다
                    (candidate.Priority == best.Priority && candidate.Deadline == best.Deadline && candidate.Reward == best.Reward && candidate.Id < best.Id);  //4. 다 같고 아이디가 적다]

                //더 좋은 주문이면 후보 교체
                if (better) best = candidate;

            }



            // TODO 08: bestOrder를 첫 주문으로 초기화하세요.

            // TODO 09: Priority가 높은 주문을 우선하세요.

            // TODO 10: Priority가 같으면 Deadline이 짧은 주문을 우선하세요.

            // TODO 11: 그래도 같으면 Reward가 높은 주문을 우선하세요.

            // TODO 12: 마지막 동률은 Id가 작은 주문을 선택하세요.

            return best.Id;
        }
    }
}
