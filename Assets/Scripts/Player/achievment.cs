using UnityEngine;
using System.Collections;

//ADD TO MAIN CAMERA

public class achievment : MonoBehaviour {

	string[] achivmentString;
	// Use this for initialization
	void Start () 
	{
		usedAchivAlready = new int[100][];
		for(int i=0; i<100; i++)
		{
			usedAchivAlready[i]=new int[100];
			//for(int i2=0; i<5; i++)


		}
		int recentScore = PlayerPrefs.GetInt ("recentResault"); //getting data from game before mini game
		if (recentScore > 0) // checking if this is start game or load after mini game
		{
			for(int i=0; i<5; i++)
			{
				for(int i2=0; i2<5; i2++)
				{
					usedAchivAlready[i][i2]=PlayerPrefs.GetInt("achiv"+i+i2);
				}
			}

		}

		achivmentString = new string[31];
		achivmentString [0] = "Help 5 pacients";
		achivmentString [1] = "Kill 3 pacients";
		achivmentString [2] = "Use cannon 5 times";
		achivmentString [3] = "Walk 50 metres";
		achivmentString [4] = "Use 4 items";
		achivmentString [5] = "Stun other doctorContainer 3 times";
		achivmentString [6] = "Be the worst doctorContainer";
		achivmentString [7] = "Don't use cannon at all";
		achivmentString [8] = "Don't use any item";
		achivmentString [9] = "Don't lose any mini-game";
		achivmentString [10] = "Be the first doctorContainer with success ";
		achivmentString [11] = "Be the laziest doctorContainer";
		achivmentString [12] = "Jump on wall 3 times";
		achivmentString [13] = "Error 303";
		achivmentString [14] = "Kill first pacient";
		achivmentString [15] = "Walk 100 metres";
		achivmentString [16] = "Help 7 pacients";
		achivmentString [17] = "Kill 4 pacients";
		achivmentString [18] = "Use 10 items";
		achivmentString [19] = "Skip 1 turn";
		achivmentString [20] = "Skip 3 turns";
		achivmentString [21] = "Go through 10 doors";
		achivmentString [22] = "Go through 15 doors";
		achivmentString [23] = "Save pacient on last life";
		achivmentString [24] = "Walk 150 metres";
		achivmentString [25] = "Save pacient without lose";
		achivmentString [26] = "Resurrect 2 pacients";
		achivmentString [27] = "Resurrect 4 pacients";
		achivmentString [28] = "Find 3 battery";
		achivmentString [29] = "Help and kill at least one pacient";
		achivmentString [30] = "Be the best sportsmen";









		randomAchiv ();
		StartCoroutine (waitToCheckAchives ());
	//	winAchiv (2,0);


	}

	void resumeAchivsLeft()
	{
		int score = 0;
		int idToAdd = 1;
		//int idPlayer = PlayerPrefs.GetInt ("currentPlayer");

		for(int i=1; i<=10; i++)
		{
			if(i==1)
				score=PlayerPrefs.GetInt("score"+i);
			else
			{
				if(PlayerPrefs.GetInt("score"+i)<score)
				{
					idToAdd=i;
				}
				else if(PlayerPrefs.GetInt("score"+i)==score)
				{
					idToAdd=0;
				}
			}
			if(i==10)
				winAchiv(7,idToAdd);


			if(PlayerPrefs.GetInt("howManyCannon"+i)==0)
			{
				idToAdd=i;
				winAchiv(8,idToAdd);
			}
			if(PlayerPrefs.GetInt("howManyItemsUse"+i)==0)
			{
				idToAdd=i;
				winAchiv(9,idToAdd);
			}
			if(PlayerPrefs.GetInt("howManyLose"+i)==0)
			{
				idToAdd=i;
				winAchiv(10,idToAdd);
			}

			if(i==1)
				score=PlayerPrefs.GetInt("howManyWalk"+i);
			else
			{
				if(PlayerPrefs.GetInt("howManyWalk"+i)<score)
				{
					idToAdd=i;
				}
				else if(PlayerPrefs.GetInt("howManyWalk"+i)==score)
				{
					idToAdd=0;
				}
			}
			if(i==10)
				winAchiv(12,idToAdd);

			if(PlayerPrefs.GetInt("howManyLose"+i)==0)
			{
				idToAdd=i;
				winAchiv(26,idToAdd);
			}

			if(i==1)
				score=PlayerPrefs.GetInt("howManyWalk"+i);
			else
			{
				if(PlayerPrefs.GetInt("howManyWalk"+i)>score)
				{
					idToAdd=i;
				}
				else if(PlayerPrefs.GetInt("howManyWalk"+i)==score)
				{
					idToAdd=0;
				}
			}
			if(i==10)
				winAchiv(31,idToAdd);

		}
	}

	void checkAchives()
	{
		int idPlayer = PlayerPrefs.GetInt ("currentPlayer");

		if(PlayerPrefs.GetInt("howManySave"+idPlayer)==5)
		{
			winAchiv(1,0);
		}
		if(PlayerPrefs.GetInt("howManyKill"+idPlayer)==5)
		{
			winAchiv(2,0);
		}
		if(PlayerPrefs.GetInt("howManyCannon"+idPlayer)==5)
		{
			winAchiv(3,0);
		}
		if(PlayerPrefs.GetInt("howManyWalk"+idPlayer)==50)
		{
			winAchiv(4,0);
		}
		if(PlayerPrefs.GetInt("howManyItemsUse"+idPlayer)==4)
		{
			winAchiv(5,0);
		}
		if(PlayerPrefs.GetInt("howManyStune"+idPlayer)==3)
		{
			winAchiv(6,0);
		}
		if(PlayerPrefs.GetInt("firstSave")==1) 
		{
			winAchiv(11,0);
			PlayerPrefs.SetInt("firstSave",2);
		}

		if(PlayerPrefs.GetInt("howManyOnWall"+idPlayer)==3)
		{
			winAchiv(13,0);
		}

		if(PlayerPrefs.GetInt("firstKill")==1)
		{
			winAchiv(15,0);
			PlayerPrefs.SetInt("firstKill",2);
		}
		if(PlayerPrefs.GetInt("howManyWalk"+idPlayer)==100)
		{
			winAchiv(16,0);
		}
		if(PlayerPrefs.GetInt("howManySave"+idPlayer)==7)
		{
			winAchiv(17,0);
		}
		if(PlayerPrefs.GetInt("howManyKill"+idPlayer)==4)
		{
			winAchiv(18,0);
		}
		if(PlayerPrefs.GetInt("howManyItemsUse"+idPlayer)==4)
		{
			winAchiv(19,0);
		}

		if(PlayerPrefs.GetInt("howManySkip"+idPlayer)==1)
		{
			winAchiv(20,0);
		}
		if(PlayerPrefs.GetInt("howManySkip"+idPlayer)==3)
		{
			winAchiv(21,0);
		}
		if(PlayerPrefs.GetInt("howManyDoors"+idPlayer)==10)
		{
			winAchiv(22,0);
		}
		if(PlayerPrefs.GetInt("howManyDoors"+idPlayer)==15)
		{
			winAchiv(23,0);
		}

		if(PlayerPrefs.GetInt("saveOnLastLife"+idPlayer)==1)
		{
			winAchiv(24,0);
		}
		if(PlayerPrefs.GetInt("howManyWalk"+idPlayer)==150)
		{
			winAchiv(25,0);
		}
		if(PlayerPrefs.GetInt("ressurect"+idPlayer)==2)
		{
			winAchiv(27,0);
		}
		if(PlayerPrefs.GetInt("ressurect"+idPlayer)==4)
		{
			winAchiv(28,0);
		}
		if(PlayerPrefs.GetInt("howManyBattery"+idPlayer)==3)
		{
			winAchiv(29,0);
		}

		if(PlayerPrefs.GetInt("howManySave"+idPlayer)>0 && PlayerPrefs.GetInt("howManyKill"+idPlayer)>0)
		{
			winAchiv(30,0);
		}
	}

	IEnumerator waitToCheckAchives()
	{
		yield return new WaitForSeconds (1);
		checkAchives ();
		StartCoroutine (waitToCheckAchives ());
	}

	int[][] usedAchivAlready;
	int[] lotteryAchiv;
	void randomAchiv()
	{
		lotteryAchiv = new int[10];
		for (int i=0; i<10; i++)
			lotteryAchiv[i] = -1;

		int i2 = 0;
		for(int i=0; i<5; i++)
		{
			int rand = Random.Range(1,31);
			while(rand!=lotteryAchiv[i2] && i2<i)
			{

				i2++;
			}
			if(i2!=i)
			{
				i--;

			}
			else
			{
				lotteryAchiv[i]=rand;
				usedAchivAlready[i][rand]=0;
///				Debug.Log(rand);
			}
			i2=0;


		}
	}

	Font numberL;

	int show=0;
	int whatAchiv=0;
	void winAchiv(int idAchiv, int idP)
	{
		lotteryAchiv [0] = 2;
		int canWinThis = 0;
		for(int i=0; i<5; i++)
		{
			if(idAchiv==lotteryAchiv[i] && usedAchivAlready[PlayerPrefs.GetInt("currentPlayer")][idAchiv]!=1 )
				canWinThis=1;
		}
		Debug.Log (canWinThis);
		if(canWinThis==1)
		{
			if(idP==0)
			{
				howManyAchivesInThisame++;
				show = 1;
				usedAchivAlready[PlayerPrefs.GetInt("currentPlayer")][idAchiv]=1;
				whatAchiv = idAchiv;
				int score = PlayerPrefs.GetInt ("score" + PlayerPrefs.GetInt ("currentPlayer"))+200;
				PlayerPrefs.SetInt ("score" + PlayerPrefs.GetInt("currentPlayer"), score);
				PlayerPrefs.SetInt("achiv"+PlayerPrefs.GetInt("currentPlayer")+idAchiv,1);
				//PlayerPrefs.GetInt("achiv"+i+i2);
                Players.GetPlayer(PlayerPrefs.GetInt("Player")).AddHistory(achivmentString[idAchiv]);
				StartCoroutine (disableAchiv ());
			}

		}

		canWinThis = 0;
		for(int i=0; i<5; i++)
		{
			if(idAchiv==lotteryAchiv[i])
				canWinThis=1;
		}

		if(idP>0 && canWinThis==1)
		{
			howManyAchivesInThisame++;
			show = 1;
				//usedAchivAlready[PlayerPrefs.GetInt("currentPlayer")][idAchiv]=1;
			whatAchiv = idAchiv;
			int score = PlayerPrefs.GetInt ("score" + idP)+200;
			PlayerPrefs.SetInt ("score"+idP, score);
			StartCoroutine (disableAchiv ());

		}
	}
	int howManyAchivesInThisame=-1;
	IEnumerator disableAchiv()
	{
		yield return new WaitForSeconds (2);
		show = 0;
		howManyAchivesInThisame = -1;
	}

	void OnGUI()
	{
		GUI.skin.box.padding = new RectOffset (0, 0, 10, 0);
		if(show==1)
		{
			GUI.Box(new Rect(0, 200+(howManyAchivesInThisame*50), 50,50), "<color=black><size=25>"+whatAchiv+"</size></color>");
			GUI.Box(new Rect(50, 200+(howManyAchivesInThisame*50), 250,50), "<color=black><size=25>"+achivmentString[whatAchiv-1]+"</size></color>");
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
