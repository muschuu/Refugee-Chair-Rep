using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class SolutionUI : MonoBehaviour
{

    List<int> continentsPlayerInput = new List<int>();
    int europePlayerInput;
    List<string> continentsName = new List<string>();
    List<List<string>> answers = new List<List<string>>();

    //[SerializeField] List<string> playerOkList = new List<string>();
    //[SerializeField] List<string> playerWiesoList = new List<string>();


    ResponsesToSolution responses;
    PhoneChat phoneChat;
    EuropaAnswers euAnswers;

    float mulitplikator;
    bool l�sung = false;
    bool zurL�sung = false;
    bool ersterDialogNachL�sung = false;
    bool once = true;
    public int intA = -1;

    [HideInInspector] public bool aufl�sung = false;

    private void Start()
    {
        responses = gameObject.GetComponent<ResponsesToSolution>();
        phoneChat = gameObject.GetComponent<PhoneChat>();
        mulitplikator = responses.mulitplikator;
    }


    public void CompareSolutions()
    {
        euAnswers = gameObject.GetComponent<EuropaAnswers>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Continent");
        objectsWithTag.OrderBy(x => x);
        foreach (GameObject continent in objectsWithTag)
        {
            Continents c = continent.GetComponent<Continents>();
            continentsPlayerInput.Add(c.slotmananger.currSlot);
            continentsName.Add(c.continentName);
            if (c.continentName == "Europa")
            {
                europePlayerInput = c.slotmananger.currSlot;
            }
        }

    }
    void WriteSolution()
    {

        ////int currSet = continentsPlayerInput[intA];
        // int currRight = responses.continentsRightAnswer[intA];
        string response = "";
        //if (currSet == currRight)
        //{
        //    // Debug.Log(continentsName[intA] + continentsPlayerInput[intA] + responses.continentsRightAnswer[intA]);

        //    response = answers[intA][1] + continentsName[intA] + ". Der Kontinent hatte: " + responses.continentsRightAnswer[intA]
        //                + " slots, das sind " + responses.continentsRightAnswer[intA] * mulitplikator + " und du hast das getippt: "
        //                + continentsPlayerInput[intA] * mulitplikator + ", das sind " + continentsPlayerInput[intA] + " slots.";
        //}
        //else if (Mathf.Abs(currSet - currRight) <= 1)
        //{
        //    // Debug.Log(continentsName[intA] + continentsPlayerInput[intA] + responses.continentsRightAnswer[intA]);
        //    response = answers[intA][1] + continentsName[intA] + ". Der Kontinent hatte: " + responses.continentsRightAnswer[intA]
        //                + " slots, das sind " + responses.continentsRightAnswer[intA] * mulitplikator + " und du hast das getippt: "
        //                + continentsPlayerInput[intA] * mulitplikator + ", das sind " + continentsPlayerInput[intA] + " slots.";
        //}
        //else if (currSet - currRight <= 2)
        //{
        //    //  Debug.Log("mehr oder weniger");

        //    //  Debug.Log(continentsName[intA] + continentsPlayerInput[intA] + responses.continentsRightAnswer[intA]);

        //    response = answers[intA][0] + continentsName[intA] + ". Der Kontinent hatte: " + responses.continentsRightAnswer[intA]
        //                + " slots, das sind " + responses.continentsRightAnswer[intA] * mulitplikator + " und du hast das getippt: "
        //                + continentsPlayerInput[intA] * mulitplikator + ", das sind " + continentsPlayerInput[intA] + " slots.";
        //}

        //else if (currSet - currRight >= 2)
        //{
        //    //Debug.Log("mehr oder weniger");
        //    // Debug.Log(continentsName[intA] + continentsPlayerInput[intA] + responses.continentsRightAnswer[intA]);

        //    response = answers[intA][2] + continentsName[intA] + ". Der Kontinent hatte: " + responses.continentsRightAnswer[intA]
        //                + " slots, das sind " + responses.continentsRightAnswer[intA] * mulitplikator + " und du hast das getippt: "
        //                + continentsPlayerInput[intA] * mulitplikator + ", das sind " + continentsPlayerInput[intA] + " slots.";
        //}
        phoneChat.SpawnTextBox(response, false);

        intA++;




    }
    string WriteEuropeSolution()
    {
        string solution = "";
        int currSet = europePlayerInput;
        int currRight = responses.continentsRightAnswer[2];
        if (currSet == currRight)
        {
            solution = euAnswers.EuGenau;

        }
        else if (Mathf.Abs(currSet - currRight) <= 1)
        {
            solution = euAnswers.EuAbweichung;

        }
        else if (currSet - currRight <= 2)
        {
            solution = euAnswers.EuWenig;

        }

        else if (currSet - currRight >= 2)
        {

            solution = euAnswers.EuZuviel;
        }
        return solution;

    }

    public void AnswerManagerVoidCall(int textBox)
    {
        if (textBox == 1)
        {
            if (aufl�sung == false) // Bevor die Aufl�sung kommt
            {
                if (intA == -1)
                {
                    // erster chat vom NPC
                    StartCoroutine(AnswerManagerVoid("begin"));
                }
                else
                {
                    // normaler dialog (Introduction / Tutorial)
                    if (intA == responses.introOver - 1)
                    {
                        aufl�sung = false;
                        StartCoroutine(AnswerManagerVoid("dialog ende"));
                    }
                    else
                    {
                        StartCoroutine(AnswerManagerVoid("dialog"));
                    }
                }
            }
            else
            {
                //  Debug.Log(responses.introOver);
                if (intA + 1 == responses.introOver)
                {
                    //  Debug.Log("Erster Dialog bei der Aufl�sung");
                    // Erster Dialog bei der Aufl�sung
                    phoneChat.MenuButtonActive(false);
                    StartCoroutine(AnswerManagerVoid("pr�fen"));

                }
                else if (zurL�sung == true)
                {
                    if (l�sung == false)
                    {
                        GameObject.FindObjectOfType<LevelUpdate>().UpdateRightSlots();
                        intA++;
                        phoneChat.newMessage.SetActive(false);
                    }
                    StartCoroutine(AnswerManagerVoid("dialog2"));
                    phoneChat.MenuButtonActive(true);
                    // jetzt kommt die personalisierte Antwort

                }
                else
                {
                    // Hier kommt der normale dialog dann
                    ersterDialogNachL�sung = true;
                    if (once)
                    {
                        once = false;
                    }
                    if (responses.button1ListButton[intA] == "Zeig her!")
                    {
                        StartCoroutine(AnswerManagerVoid("solution"));

                        zurL�sung = true;

                    }
                    else
                    {
                        StartCoroutine(AnswerManagerVoid("dialog"));
                    }

                }

            }
        }
        else
        {
            //Debug.Log(responses.button2ListButton[intA]);
            // Debug.Log(intA);
            // Debug.Log(responses.button2ListButton[intA - 1]);

            if (responses.button2ListButton[intA] == "Zum Spiel")
            {
                // Hier wird das Handy nach oben geschoben
                StartCoroutine(AnswerManagerVoid("handyweg"));

            }
            else if (responses.button2ListButton[intA] == "N�chstes Level")
            {
                StartCoroutine(AnswerManagerVoid("nextlvl"));

                //Hier geht es zu dem n�chstem level
            }
            else if (responses.button2ListButton[intA] == "L�s auf!")
            {
                Debug.Log("Zur l�sung");
                // Hier wird das Handy nach oben geschoben
                StartCoroutine(AnswerManagerVoid("handyweg2"));
                intA++;

            }


        }

    }
    //private void Update()
    //{
    //    Debug.Log(intA);
    //}
    IEnumerator AnswerManagerVoid(string answertype)
    {
        yield return new WaitForSecondsRealtime(.1f);

        if (answertype == "begin") //fertig
        {
            intA++;
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();

        }
        if (answertype == "begin2") //fertig
        {
            //intA--;
            intA++;
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();

        }
        else if (answertype == "solution") // noch bearbeiten 
        {
            phoneChat.MovePhoneToLeft();
            yield return new WaitForSecondsRealtime(1.2f);

            intA++;
            phoneChat.newMessage.SetActive(false);
            phoneChat.TypePlayerChat(responses.button1ListContent[intA - 1]);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(WriteEuropeSolution(), false);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();
        }
        else if (answertype == "solution2") // noch bearbeiten 
        {
            // phoneChat.TypePlayerChat(responses.button1ListContent[intA - 1]);
            //yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(WriteEuropeSolution(), false);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();
            intA++;
        }
        else if (answertype == "dialog") //fertig
        {

            phoneChat.TypePlayerChat(responses.button1ListContent[intA]);
            intA++;
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();

        }
        else if (answertype == "dialog2") //fertig
        {

            phoneChat.TypePlayerChat(responses.button1ListContent[intA]);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            intA++;
            phoneChat.EnableChatBox();

        }
        else if (answertype == "pr�fen") //
        {

            phoneChat.DisableChatBox();
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.EnableChatBox();
            //  intA++;

        }
        else if (answertype == "dialog ende") // fertig
        {

            phoneChat.TypePlayerChat(responses.button1ListContent[intA]);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton1List[intA], false);
            if (SpawnImage())
            {
                yield return new WaitForSecondsRealtime(.7f);
                phoneChat.SpawnImageBox(responses.images1Button[intA]);
            }
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.DisableChatBox();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.OnClickHidePhone();
            //intA++;
            // intA = 0;

        }
        else if (answertype == "handyweg")
        {
            Debug.Log("handyweg");
            phoneChat.TypePlayerChat(responses.button2ListContent[intA]);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton2List[0], false);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnImageBox(responses.images2Button[0]);
            yield return new WaitForSecondsRealtime(2f);
            phoneChat.OnClickHidePhone();
            phoneChat.EnableChatBox();


        }
        else if (answertype == "handyweg2")
        {
            Debug.Log("handyweg2");

            phoneChat.TypePlayerChat(responses.button2ListContent[intA - 1]);
            phoneChat.SolutionScreen();

            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(responses.npcButton2List[2], false);
            yield return new WaitForSecondsRealtime(2.6f);
            l�sung = true;
            GameObject.FindObjectOfType<LevelUpdate>().UpdateRightSlots();
            phoneChat.OnClickHidePhone();
            yield return new WaitForSecondsRealtime(1.2f);
            phoneChat.EnableChatBox();
            yield return new WaitForSecondsRealtime(7.6f);
            if (ersterDialogNachL�sung == false)
            {
                phoneChat.newMessage.SetActive(true);

                StartCoroutine(AnswerManagerVoid("begin2"));
                intA--;
            }
            //  intA++;

        }
        else if (answertype == "nextlvl")
        {
            Debug.Log("Alle Nachrichten, die noch nicht reingestellt wurden kommen hier dann schnell");
            phoneChat.TypePlayerChat(responses.button2ListContent[intA]);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.MenuButtonActive(true);
            phoneChat.SpawnTextBox(responses.npcButton2List[1], false);
            yield return new WaitForSecondsRealtime(4.6f);
            phoneChat.OnClickHidePhone();
            yield return new WaitForSecondsRealtime(1f);

            if (responses.npcButton1List.Count > intA)
            {
                int lengh = responses.npcButton1List.Count;
                for (int i = intA; i < lengh; i++)
                {
                    Debug.Log(i);
                    phoneChat.TypePlayerChat(responses.button1ListContent[i]);
                    // yield return new WaitForSecondsRealtime(.5f);
                    phoneChat.SpawnTextBox(responses.npcButton1List[i], false);

                    intA++;
                }
            }
            yield return new WaitForSecondsRealtime(.8f);

            LevelManager.instance.UpdateNextLevel(FindObjectOfType<LevelUpdate>().level);
            LevelManager.instance.OpenNextLevel(gameObject.transform.parent.GetComponent<LevelUpdate>().level + 1);
        }


    }
    bool SpawnImage()
    {
        if (responses.images1Button[intA] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

