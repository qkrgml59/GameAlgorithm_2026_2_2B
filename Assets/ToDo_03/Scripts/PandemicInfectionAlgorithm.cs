using System.Collections.Generic;
using AlgoCourse.Lesson3;

namespace AlgoCourse.StudentWork
{
    public sealed class PandemicInfectionAlgorithm : ICityInfectionAlgorithm
    {
        private const int MaximumlnfectionLevel = 3;

        private readonly Dictionary<int, int[]> graph = new Dictionary<int, int[]>();
        private readonly Dictionary<int, int> infectionLevels = new Dictionary<int, int>();

        private readonly Queue<int> infectionQueue = new Queue<int>();
        private readonly HashSet<int> outbreakCities = new HashSet<int>();


        // TODO 01: 도시 그래프, 감염 단계, 감염 요청 Queue를 생성합니다.
        // TODO 02: 한 번의 연쇄 감염에서 Outbreak한 도시를 기록합니다.
        public int PendingCount => 0;
        public int OutbreakCount { get; private set; }

        public void Initialize(IReadOnlyDictionary<int, int[]> cityGraph)
        {
            // TODO 03: 그래프를 복사하고 모든 도시의 감염 단계를 0으로 만듭니다.
            graph.Clear();
            infectionLevels.Clear();
            infectionQueue.Clear();
            outbreakCities.Clear();

            OutbreakCount = 0;

            //전달 받은 모든 도시 정보 확인
            foreach (KeyValuePair<int, int[]> city in cityGraph)
            {
                graph.Add(city.Key, city.Value);
                infectionLevels.Add(city.Key, 0);
            }
        }

      
        public bool QueueInfection(int cityId)
        {
            // TODO 04: 존재하는 도시의 감염 요청을 Enqueue합니다.
            if(!graph.ContainsKey(cityId))
            {
                return false;
            }

            if(infectionQueue.Count ==0)
            {
                outbreakCities.Clear();
            }

            infectionQueue.Enqueue(cityId);

            return false;
            
        }

        public CityInfectionStep ProcessNext()
        {
            // TODO 05: Dequeue 후 감염 단계를 증가시키거나 Outbreak를 처리합니다.
            if(infectionQueue.Count ==0)
            {
                return new CityInfectionStep(-1, 0, false, false);
            }

            int cityId = infectionQueue.Dequeue();
            int currentLevel = infectionLevels[cityId];

            if(currentLevel < MaximumlnfectionLevel)
            {
                int nextLevel = currentLevel + 1;
                infectionLevels[cityId] = nextLevel;
                return new CityInfectionStep(cityId, nextLevel, false, true);
            }

            if(!outbreakCities.Add(cityId))
            {
                return new CityInfectionStep(cityId, currentLevel, false, false);
            }

            OutbreakCount++;

            foreach(int neighborld in graph[cityId])
            {
                infectionQueue.Enqueue(neighborld);
            }

            return new CityInfectionStep(cityId, currentLevel, true, true);
        }

        public int GetInfectionLevel(int cityId)
        {
            // TODO 06: 도시의 현재 감염 단계를 반환합니다.
            return infectionLevels.TryGetValue(cityId, out int level)? level : 0;
        }
    }
}
