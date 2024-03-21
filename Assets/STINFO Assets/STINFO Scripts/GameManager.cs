using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator transition;
    public GameObject startButton, homeButton, exitGameButton, backButton, nextButton, slideDisplay;
    public List<STINFOQuestionsAndAnswers> QnA;
    public List<bool> userAnswerList;
    public List<string> slideHeader = new List<string>();
    public List<string> slideInfo = new List<string>();
    public TMP_Text currentSlideHeader, currentSlideInfo;
    public int counterSH = 0, counterSI = 0;
    public int questionCounter;

    //initializes the list of question and slide infomation
    void Start()
    {
        //slide 3
        slideHeader.Add("WHAT IS STINFO");
        slideInfo.Add("STINFO stands for Scientific and Technical INFOrmation \nSTINFO is information related to experimental, developmental, or " +
            "engineering works and applies to newly created engineering drawings, " +
            "engineering data and associated lists, standards, specifications, " +
            "technical manuals, technical reports, technical orders, blueprints, plans, " +
            "instructions, computer software and documentation, catalog - item " +
            "identifications, data sets, studies and analyses, and other technical " +
            "information that can be used or be adapted for use to design, engineer, " +
            "produce, manufacture, operate, repair, overhaul, or reproduce any " +
            "military or space equipment or technology concerning such equipment. " +
            "The data may be in tangible form, such as a model, prototype, blueprint, " +
            "photograph, plan, instruction, or an operating manual, or may be " +
            "intangible, such as a technical service or oral, auditory, or visual descriptions. ");

        //slide 4
        slideHeader.Add("WHAT IS NOT STINFO");
        slideInfo.Add("- Regulations & Policies " +
            "\n- Gov’t Office Processes & Administrative Plans" +
            "\n- Cryptographic / Communications Security (COMSEC) info(DoDI 8523.01)" +
            "\n- Communications / Electronic / Signals Intelligence (SIGINT) info" +
            "\n- Air Force Day - to - Day Warfighter Operation Plans (Unless Required for SCI / Tech Analysis)" +
            "\n\nThese documents should be protected, but NOT under STINFO policy");

        //slide 5
        slideHeader.Add("STINFO CREATED BY AFSC");
        slideInfo.Add("STINFO Types" +
            "\n- TOs (Technical Order)" +
            "\n- POs (Process Orders" +
            "\n- Engineering Drawings" +
            "\n- SW Doc" +
            "\n- Tech Data Packages" +
            "\n- TDIP Documents (Technology Development and Insertion Process)" +
            "\n- Briefings (if they contain info from one of the above!)");

        //slide 6
        slideHeader.Add("IMPACT OF UNAUTHORIZED STINFO DISTRIBUTION");
        slideInfo.Add("Unauthorized access to STINFO can cause damage to:" +
            "\n- U.S. national interests and security" +
            "\n- Defense programs / DoD investment" +
            "\n- Defense contractor Intellectual Property rights" +
            "\n\nADVANCED DOD/MILITARY TECHNOLOGY INFORMATION MADE AVAILABLE TO ADVERSARIES COMPROMISES U.S. MILITARY ADVANTAGE");

        //slide 7
        slideHeader.Add("CDO RESPONSIBILITY");
        slideInfo.Add("The Controlling DoD Office (CDO) is the office that:" +
            "\n- CREATES (OR PROCURES) or is CONSIGNED the document" +
            "\n- DECIDES appropritate audience for secondary distribution and applies statement" +
            "\n- APPLIES DISTRIBUTION MARKINGS to control STINFO, contractors do not decide which distribution statement to apply" +
            "\n- REVIEWS AND DECIDES, as the KNOWLEDGE OWNER, on distribution to requesters outside the distribution statement audience" +
            "\n- IS RESPONSIBLE for deciding if/when to change the distribution statement and changing, or instructing any necessary marking changes or corrections" +
            "\n- INCLUDES: SW personnel, equipment specialists, engineers, T.O. managers, and liaisons");

        //slide 8
        slideHeader.Add("DISTRIBUTING STINFO");
        slideInfo.Add("Two categories of distribution are:" +
            "\n- PRIMARY: from the Controlling DoD Office. The initial targeted distribution of or access to technical documents authorized byt the controlling DoD office or any release by the controlling office thereafter. The CDO can distribute to anyone it determines is eligible to receive." +
            "\n- SECONDARY: release of technical documents provided after primary distribution by the originator or controlling office. It includes loaning, allowing the reading of, or releaseing a document outright, in whole,  or in part. Distribution by anyone other than the CDO... " +
            "e.g., DTIC, ASSIST, TO, and drawing repositories, or anyone who's not the CDO. These personnel must either adhere to the distribution statement or contact the CDO for approval to distribute outside the distribution statement.");

        //slide 10
        slideHeader.Add("AUTHORIZED DISTRIBUTION STATEMENTS");
        slideInfo.Add("DISTRIBUTION STATEMENT A: Approved for public release; distribution is unlimited" +
            "\n- DISTRIBUTION STATEMENT B: Distribution authorized to U.S.Government Agencies only (reason), (date of determination).Refer other requests for this document to(controlling DoD office)" +
            "\n- DISTRIBUTION STATEMENT C: Distribution authorized to U.S.Government Agencies and their contractors(reason), (date of determination).Other requests for this document shall be referred to(controlling DoD office). " +
            "\n- DISTRIBUTION STATEMENT D: Distribution authorized to DoD and U.S.DoD contractors only (reason), (date of determination).Other requests for this document shall be referred to(controlling DoD office)." +
            "\n- DISTRIBUTION STATEMENT E: Distribution authorized to DoD components only(reason), (date of determination).Other requests for this document shall be referred to(controlling DoD office)." +
            "\n- DISTRIBUTION STATEMENT F: Further dissemination only as directed by(controlling DoD office), (date of determination) or higher DoD authority" +
            "\n\nIMPORTANT: Before any STINFO can be marked with Distribution Statement A, it MUST be reviewed and approved by the Public Affairs Office - see your liaison for local process.");

        //slide 13
        slideHeader.Add("EXPORT CONTROL MARKING");
        slideInfo.Add("Data subject to export control IAW:" +
            "\n- International Traffic in Arms regulations (ITAR)" +
            "\n- Export Administration Act(EAR)" +
            "\n- United States Munitions List(USML)" +
            "\n- Commerce Control List(CCL)" +
            "\n\nMust have have the Export Control Warning Statement Label");

        //slide 14
        slideHeader.Add("WHERE TO MARK STINFO DOCUMENTS");
        slideInfo.Add("\n- DVD-ROM with case: Distribution Statement is placed on both the DVD and its case." +
            "\n- Presentation: A Distribution Statement and Export Control Warning Notice, if applicable, must be on each slide in a presentation" +
            "\n- Document: Distribution Statements must be placed on a conspicuous location on the cover, title page, or on the front page.A Distribution Statement must appear on each printed page of a digitally stored document." +
            "\n- Engineering drawings, Distribution Statement must be on all single-sheet drawings and associated lists, and on sheet one of multi - sheet documents.");

        //slide 15
        slideHeader.Add("REMEMBER TO ENCRYPT");
        slideInfo.Add("AFI 61-201, paragraph 1.3.4.13 states:" +
            "\n\n\"Protect STINFO(i.e., Distribution B, C, D, E, and F) documents by encryption when transmitting via email IAW the following: AFI 10 - 701, \"Operations Security (OPSEC)\" and AFMAN 33 - 152, \"User Responsibilities and Guidance for Information Systems.\"");


        //slide 16
        slideHeader.Add("TYPES OF INTELLECTUAL PROPERTY RIGHTS");
        slideInfo.Add("Six types of Intellectual Property (IP) rights are relevant when working with STINFO:" +
            "\n- Unlimited rights" +
            "\n- Limited rights" +
            "\n- Government purpose rights" +
            "\n- Restricted rights" +
            "\n- Specially negotiated rights, and" +
            "\n- Small business innovation and research rights.");

        //slide 18
        slideHeader.Add("DESTROYING UNCLASSIFIED STINFO DOCUMENTS");
        slideInfo.Add("Effective ways to Dispose unclassefied, limited-distribution STINFO:" +
            "\n- Paper documents - shredding or burning only" +
            "\n- Floppy disks - reformatting and breaking into four or more pieces" +
            "\n- Hard drives - reformatting and sending to a recycling center » hard drives" +
            "\n- CD - ROMS / DVDs - shredding or breaking into 4 or more pieces" +
            "\n- Memory wands / flash drives - deleting files from memory and breaking into two or more pieces" +
            "\n- Magnetic tape - deleting the files and sending to a recycling center");
    }

    // Update is called once per frame
    void Update()
    {
        currentSlideHeader.text = slideHeader[counterSH];
        currentSlideInfo.text = slideInfo[counterSH];
    }

    //enter the whiteboard
    public void zoomIn()
    {
        startButton.SetActive(false);
        homeButton.SetActive(false);
        transition.SetTrigger("Start");
        exitGameButton.SetActive(true);
        backButton.SetActive(true);
        nextButton.SetActive(true);
        slideDisplay.SetActive(true);
    }

    //exit out of the whiteboard
    public void zoomOut()
    {
        slideDisplay.SetActive(false);
        exitGameButton.SetActive(false);
        backButton.SetActive(false);
        nextButton.SetActive(false);
        transition.SetTrigger("Start");
        homeButton.SetActive(true);
        startButton.SetActive(true);
    }
}
