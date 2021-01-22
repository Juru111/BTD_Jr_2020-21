using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingMenager : MonoBehaviour
{
    //dla bloonów
    [System.Serializable]
    public class BloonTypeInfo
    {
        public BloonTypes name;
        public GameObject prefab;
        public int startSize;
    }
    [SerializeField]
    private Transform bloonsAnchor;
    [SerializeField]
    private List<BloonTypeInfo> bloonTypeInfos;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> bloonTypesDictionary;
    private int bloonNrInList;

    //dla projectilesów
    [System.Serializable]
    public class ProjectileTypeInfo
    {
        public ProjectileTypes name;
        public GameObject prefab;
        public int startSize = 50;
    }
    [SerializeField]
    private Transform projectilesAnchor;
    [SerializeField]
    private List<ProjectileTypeInfo> projectileTypeInfos;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> projectileTypesDictionary;

    //dla PoP ikonek
    [System.Serializable]
    public class PopIconInfo
    {
        public GameObject prefab;
        public int startSize;
    }
    [SerializeField]
    private Transform popIconsAnchor;
    [SerializeField]
    private PopIconInfo popIconInfo;
    Queue<GameObject> popPool = new Queue<GameObject>();

    void Awake()
    {
        bloonTypesDictionary = new Dictionary<string, Queue<GameObject>>();
        projectileTypesDictionary = new Dictionary<string, Queue<GameObject>>();

        //Wypełnianie wszystkich pool-i początkową liczbą obiektów
        //|najpierw bloony, potem projectilesy|

        //bloony
        foreach (BloonTypeInfo type in bloonTypeInfos)
        {
            Queue<GameObject> bloonPool = new Queue<GameObject>();

            for(int i = 0; i < type.startSize; i++)
            {
                GameObject bloon = Instantiate(type.prefab, bloonsAnchor);
                bloon.SetActive(false);
                bloonPool.Enqueue(bloon);
            }

            bloonTypesDictionary.Add(type.name.ToString(), bloonPool);
        }

        //projectilesy
        foreach (ProjectileTypeInfo type in projectileTypeInfos)
        {
            Queue<GameObject> projectilePool = new Queue<GameObject>();

            for (int i = 0; i < type.startSize; i++)
            {
                GameObject projectile = Instantiate(type.prefab, projectilesAnchor);
                projectile.SetActive(false);
                projectilePool.Enqueue(projectile);
            }

            projectileTypesDictionary.Add(type.name.ToString(), projectilePool);
        }

        //Pop Icons
        for (int i = 0; i < popIconInfo.startSize; i++)
        {
            GameObject popIcon = Instantiate(popIconInfo.prefab, projectilesAnchor);
            popIcon.SetActive(false);
            popPool.Enqueue(popIcon);
        }

        //projectileTypesDictionary.Add(type.name.ToString(), projectilePool);
    }

    public void SummonBloon(BloonTypes bloonName, int layersLeft, Vector3 position ,int myNextWaypoint,
                            float distanceToWaypoint, bool isCammo, float stunDurationLeft, List<GameObject> parentPopProjectles)
    {
        if (bloonTypesDictionary[bloonName.ToString()].Count > 0)
        {
            GameObject bloonToSummon = bloonTypesDictionary[bloonName.ToString()].Dequeue();
            bloonToSummon.SetActive(true);

            if(bloonToSummon.TryGetComponent<Bloon>(out Bloon bloonComponent))
            {
                var newParentPopProjectles = new List<GameObject>();
                foreach (var item in parentPopProjectles)
                {
                    newParentPopProjectles.Add(item);
                }
                bloonComponent.SetMe(bloonName, layersLeft, position, myNextWaypoint, distanceToWaypoint, isCammo, stunDurationLeft, newParentPopProjectles);
            }
            else
            { Debug.LogError(bloonToSummon + " nie posiada komponentu Bloon!"); }
                
        }
        else //jeśli jest za mało obiektów w pamięci
        {
            //dospawnowuje w Kolejkę zapas
            for (int i = 0; i < 100; i++)
            {
                if(bloonName == BloonTypes.White)
                { bloonNrInList = 10; }
                else if(bloonName == BloonTypes.Lead)
                { bloonNrInList = 11; }
                else
                { bloonNrInList = (int)bloonName - 1; }


                GameObject bloon = Instantiate(bloonTypeInfos[bloonNrInList].prefab, bloonsAnchor);
                bloon.SetActive(false);
                bloonTypesDictionary[bloonName.ToString()].Enqueue(bloon);
            }

            GameObject bloonToSummon = bloonTypesDictionary[bloonName.ToString()].Dequeue();
            bloonToSummon.SetActive(true);

            if (bloonToSummon.TryGetComponent<Bloon>(out Bloon bloonComponent))
            {
                bloonComponent.SetMe(bloonName, layersLeft, position, myNextWaypoint, distanceToWaypoint, isCammo, stunDurationLeft, parentPopProjectles);
            }
            else
            { Debug.LogError(bloonToSummon + " nie posiada komponentu Bloon!"); }
        }
        
    }

    public void SummonProjectile(ProjectileTypes projectileName, Vector3 position, int pierce, int power,
                                float movementSpeed, float rotationnAngle, float range, bool canHitCamo)
    {
        //jeśli w jest dostępny taki projectile to go summonuje
        if (projectileTypesDictionary[projectileName.ToString()].Count > 0)
        {
            GameObject projectileToSummon = projectileTypesDictionary[projectileName.ToString()].Dequeue();
            projectileToSummon.SetActive(true);

            if (projectileToSummon.TryGetComponent<Projectile>(out Projectile projectileComponent))
            {
                projectileComponent.SetMe(projectileName, position, pierce, power, movementSpeed, rotationnAngle, range, canHitCamo);
            }
            else
            { Debug.LogError(projectileToSummon + " nie posiada komponentu Projectile!"); }
        }
        else //jeśli jest za mało obiektów w pamięci
        {
            //dospawnowuje w Kolejkę zapas
            for (int i = 0; i < 100; i++)
            {
                GameObject projectile = Instantiate(projectileTypeInfos[(int)projectileName - 1].prefab, projectilesAnchor);
                projectile.SetActive(false);
                projectileTypesDictionary[projectileName.ToString()].Enqueue(projectile);
            }

            GameObject projectileToSummon = projectileTypesDictionary[projectileName.ToString()].Dequeue();
            projectileToSummon.SetActive(true);

            if (projectileToSummon.TryGetComponent<Projectile>(out Projectile projectileComponent))
            {
                projectileComponent.SetMe(projectileName, position, pierce, power, movementSpeed, rotationnAngle, range, canHitCamo);
            }
            else
            { Debug.LogError(projectileToSummon + " nie posiada komponentu Projectile!"); }
        }

    }

    public void SummonPop(Vector3 _position)
    {
        if(popPool.Count > 0)
        {
            GameObject popIconToSummon = popPool.Dequeue();
            popIconToSummon.SetActive(true);
            popIconToSummon.transform.position = _position;
        }
        else
        {
            for (int i = 0; i < popIconInfo.startSize/2; i++)
            {
                GameObject popIcon = Instantiate(popIconInfo.prefab, projectilesAnchor);
                popIcon.SetActive(false);
                popPool.Enqueue(popIcon);
            }

            GameObject popIconToSummon = popPool.Dequeue();
            popIconToSummon.SetActive(true);
            popIconToSummon.transform.position = _position;
        }
    }

    //te metody wywołuje sam obiekt który się odkłąda
    public void ReturnBloon(GameObject bloon, BloonTypes bloonName)
    {
        bloonTypesDictionary[bloonName.ToString()].Enqueue(bloon);
        bloon.SetActive(false);
    }
    public void ReturnProjectile(GameObject projectile, ProjectileTypes projectileName)
    {
        projectileTypesDictionary[projectileName.ToString()].Enqueue(projectile);
        projectile.SetActive(false);
    }
    public void ReturnPopIcon(GameObject popIcon)
    {
        popPool.Enqueue(popIcon);
        popIcon.SetActive(false);
    }

}
