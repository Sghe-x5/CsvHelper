using System.Collections.Generic;

namespace ClassLibrary
{
    public class Organism
    {
        private int organismId;
        private string scientificName;
        private string commonName;
        private string kingdom;
        private string habitat;
        private int population;
        private List<string> characteristics;
        private List<string> researchers;

        // Конструктор без параметров
        public Organism()
        {
            organismId = 0;
            scientificName = string.Empty;
            commonName = string.Empty;
            kingdom = string.Empty;
            habitat = string.Empty;
            population = 0;
            characteristics = new List<string>();
            researchers = new List<string>();
        }

        // Конструктор с параметрами
        public Organism(int organismId, string scientificName, string commonName, string kingdom, string habitat, int population, List<string> characteristics, List<string> researchers)
        {
            this.organismId = organismId;
            this.scientificName = scientificName;
            this.commonName = commonName;
            this.kingdom = kingdom;
            this.habitat = habitat;
            this.population = population;
            this.characteristics = characteristics;
            this.researchers = researchers;
        }

        // Публичные геттеры для приватных полей
        public int OrganismId => organismId;
        public string ScientificName => scientificName;
        public string CommonName => commonName;
        public string Kingdom => kingdom;
        public string Habitat => habitat;
        public int Population => population;
        public List<string> Characteristics => characteristics;
        public List<string> Researchers => researchers;
    }
}
