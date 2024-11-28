using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Button actionButtonA;
    public Button actionButtonB;
    public Button actionButtonC;
    public Button actionButtonD;
    int itemNumber = 1;
    string answer;
    List<string> answer_key = new List<string> {"C", "B", "A", "C", "A", "D", "B", "B", "C", "B", "A", "D", "A", "C", "B", "D", "B", "A", "A", "B"};
    
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);

        dialogueText.text = enemyUnit.unitName + " : Mabilisan lang 'to";
        yield return new WaitForSeconds(2f);

        // dialogueText.text = "Question #1: Mahalaga ba ang Filipino?";
        dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Kailan unang nagkaroon ng batas hinggil sa pagpapaunlad at pagpapatibay ng isang komon na wikang pambansa batay sa isa sa mga umiiral na katutubong wika sa Pilipinas?";
        yield return new WaitForSeconds(5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        if (itemNumber > answer_key.Count)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
            return;
        }

        if (itemNumber == 1)
        {
            dialogueText.text = "A. 1986\nB. 1940\nC. 1935\nD. 1901";
        } else if (itemNumber == 2)
        {
            dialogueText.text = "A. ng at nang\nB. po at opo\nC. kung at kong\nD. ma'am at sir";
        } else if (itemNumber == 3)
        {
            dialogueText.text = "A. Katitikan\nB. Sanskrito\nC. Baybayin\nD. Panitikan";
        } else if (itemNumber == 4)
        {
            dialogueText.text = "A. 1905\nB. 1972\nC. 1901\nD. 1987";
        } else if (itemNumber == 5)
        {
            dialogueText.text = "A. Mother Tongue Based Multilingual Education\nB. Foreign Elective\nC. Fil 40\nD. Speak English Only Policy";
        } else if (itemNumber == 6)
        {
            dialogueText.text = "A. eskpresyon ng kultura\nB. impukan-hanguan ng kultura\nC. agusan ng kultura\nD. pakikipag-usap sa mga dayuhan";
        } else if (itemNumber == 7)
        {
            dialogueText.text = "A. KKK\nB. La Tondena Distillery\nC. Uniteam\nD. Gomburza";
        } else if (itemNumber == 8)
        {
            dialogueText.text = "A. U\nB. L\nC. A\nD. T";
        } else if (itemNumber == 9)
        {
            dialogueText.text = "A. Touch me please\nB. Touch mo siya\nC. Touch me not\nD. Touch me now";
        } else if (itemNumber == 10)
        {
            dialogueText.text = "A. Gregorio Del Pilar\nB. Pedro Paterno\nC. Jose Rizal\nD. Diego Silang";
        } else if (itemNumber == 11)
        {
            dialogueText.text = "A. Ninay\nB. Noli Me Tangere\nC. Ibong Adarna\nD. Bernardo Carpio";
        } else if (itemNumber == 12)
        {
            dialogueText.text = "A. Manila Cathedral\nB. Fort Santiago\nC. National Museum\nD. Pambansang Aklatan ng Pilipinas";
        } else if (itemNumber == 13)
        {
            dialogueText.text = "A. Kapwa\nB. Loob\nC. Kapit-bisig\nD. Kapit-bahay";
        } else if (itemNumber == 14)
        {
            dialogueText.text = "A. Sinulog\nB. Mascara\nC. Panagbenga\nD. Ati-atihan";
        } else if (itemNumber == 15)
        {
            dialogueText.text = "A. Pilipino\nB. Tagalog\nC. Espanol\nD. Ingles";
        } else if (itemNumber == 16)
        {
            dialogueText.text = "A. Boracay, Aklan\nB. Bohol\nC. Ifugao\nD. Batanes Islands";
        } else if (itemNumber == 17)
        {
            dialogueText.text = "A. Polynesian\nB. Austronesian\nC. Indo-European\nD. Afroasiatic";
        } else if (itemNumber == 18)
        {
            dialogueText.text = "A. Jaclyn Jose\nB. Sharon Cuneta\nC. Cherie Gil\nD. Cherry Pie-Picache";
        } else if (itemNumber == 19)
        {
            dialogueText.text = "A. 1937\nB. 1987\nC. 1905\nD. 1989";
        } else if (itemNumber == 20)
        {
            dialogueText.text = "A. Anastacio Caedo\nB. Guillermo Tolentino\nC. Grace Javier-Alfonso\nD. Fernando Amorsolo";
        }
        // dialogueText.text = "Choose an action:";
        // Debug.Log("Player's turn"); // Add this line for debugging
        //Coroutines- waiting codes?..., codes that run separately from everything else... pause whenever we want
        EnableButtons(true);
    }

    IEnumerator EnemyTurn()
    {
        // AI?, logic, here- attack any time
        // dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Mahalaga ba ang Filipino?";
        if (itemNumber == 2)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Ginagamit na salita sa pagbibigay-galang.";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 3)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Alpabetong Tagalog na susundin ng lahat sa pagsulat, pananalita at iba pang pangangailangang pangwika.";
            yield return new WaitForSeconds(4f);
        } else if (itemNumber == 4)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Kailan nagsimula ang pampublikong edukasyon sa Pilipinas?";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 5)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Porma ng edukasyon, pormal o hindi pormal, kung saan ginagamit ang wikang bahay ng mag-aaral at mga karagdagang wika sa silid-aralan.";
            yield return new WaitForSeconds(4f);
        } else if (itemNumber == 6)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Ayon kay Zeus Salazar, ang wika ay maliban sa:";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 7)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Anong samahan ng manggagawa ang nagstrike at nag welga gamit ang 'Tama na, Sobra na, Welga na' noong panahon ng diktadurang Marcos?";
            yield return new WaitForSeconds(5f);
        } else if (itemNumber == 8)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Anong letra ang hand gesture na unang ginamit ni Cory sa paglaban sa rehimeng Marcos Sr?";
            yield return new WaitForSeconds(2f);
        } else if (itemNumber == 9)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Ano ang ibig sabihin ng Noli Me Tangere?";
            yield return new WaitForSeconds(2f);
        } else if (itemNumber == 10)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Sino ang unang Pilipinong manunulat ng nobela sa wikang Espanyol?";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 11)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Ano ang pamagat ng unang nobela na isinulat ng isang Pilipino?";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 12)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Itinatag bilang Museo-Biblioteca de Filipinas noong 1891 sa Intramuros, Manila.";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 13)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Isang Pilipinong konsepto ng pakikipag-ugnayan at pakikibahagi. Ito'y pagkilala at pagtanggap sa 'iba' bilang bahagi ng mas malaking 'tayo.'";
            yield return new WaitForSeconds(4f);
        } else if (itemNumber == 14)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Festival na hango mula sa Kankanaey na nangangahulugang 'panahon ng pamumukadkad.'";
            yield return new WaitForSeconds(4f);
        } else if (itemNumber == 15)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Sa anong wika sa Pilipinas nakabatay ang wikang pambansa na Filipino?";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 16)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Ano ang unang probinsya sa Pilipinas na pinarangalan ng United Nations World Tourism Organization na maging bahagi ng International Network of Sustainable Tourism Observatories?";
            yield return new WaitForSeconds(5f);
        } else if (itemNumber == 17)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Language group na sinasabing pinanggalingan ng wikang Tagalog bago ang pananakop ng mga Espanyol.";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 18)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Unang aktres na nanalo sa 69th Cannes Film Festival sa pagganap niya sa pelikulang 'Ma'Rosa'";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 19)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Anong taon pinayagan na bumoto ang mga babae sa Pilipinas?";
            yield return new WaitForSeconds(3f);
        } else if (itemNumber == 20)
        {
            dialogueText.text = enemyUnit.unitName + " asks Question #" + itemNumber + ": Sino ang iskultor ng pinakaunang bersiyon ng Oblation, natatanging simbolo ng Unibersidad ng Pilipinas?";
            yield return new WaitForSeconds(3f);
        }
        // yield return new WaitForSeconds(3f);

        // ++itemNumber;
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "GGEZ, edi ikaw na perpekto!!";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Ending");
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "Olats ka, mag-aral at maglibot ka kase muna haysstt!!";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("SampleScene");

        // Load out of battle screen...
        }
    }

    IEnumerator PlayerAttack()
    {
        EnableButtons(false);
        bool isDead = false;
        if (itemNumber - 1 < answer_key.Count && answer == answer_key[itemNumber - 1]) {
            isDead = enemyUnit.TakeDamage(playerUnit.damage);
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "Perfect!!";
        } else
        {
            
            dialogueText.text = "Bonak!!";
        }
        
        // Damage the enemy
        yield return new WaitForSeconds(2f);
        ++itemNumber;

        // Check if the enemy is dead
        if(isDead)
        {
            // End the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else if (itemNumber > answer_key.Count)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            // Enemy's turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // Change state based on what happened
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(20);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Sarughppp";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void ButtonA()
    {
        if (state != BattleState.PLAYERTURN)  //&& (itemNumber != 1))
            return;
        
        // Debug.Log("ButtonA pressed"); // Add this line for debugging
        answer = "A";
        StartCoroutine(PlayerAttack());
    }

    public void ButtonB()
    {
        if (state != BattleState.PLAYERTURN)  //&& (itemNumber != 1)
            return;
        
        // Debug.Log("ButtonB pressed"); // Add this line for debugging
        answer = "B";
        StartCoroutine(PlayerAttack());
    }

    public void ButtonC()
    {
        if (state != BattleState.PLAYERTURN)  //&& (itemNumber != 1))
            return;
        
        // Debug.Log("ButtonA pressed"); // Add this line for debugging
        answer = "C";
        StartCoroutine(PlayerAttack());
    }

    public void ButtonD()
    {
        if (state != BattleState.PLAYERTURN)  //&& (itemNumber != 1))
            return;
        
        // Debug.Log("ButtonA pressed"); // Add this line for debugging
        answer = "D";
        StartCoroutine(PlayerAttack());
    }

    void EnableButtons(bool enable)
    {
        actionButtonA.interactable = enable;
        actionButtonB.interactable = enable;
        actionButtonC.interactable = enable;
        actionButtonD.interactable = enable;
    }
}