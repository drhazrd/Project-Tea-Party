using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{
	
	public Text nameText;
	public Text dialogueText;
	
	public Animator animator;
	
	private Queue<string> sentences;
	
	void Start(){
		sentences = new Queue<string>();
	}
	public void StartDialogue(Dialogue dialogue){
		Debug.Log ("Speaking with "+ dialogue.name);
		nameText.text = dialogue.name;
		sentences.Clear();
		animator.SetBool("IsOpen", true);
		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence(){
		if (sentences.Count == 0){
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		Debug.Log(sentence);
		//dialogueText.text = sentence;
	}
	
	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}
	
	void EndDialogue(){
		Debug.Log("End of talking.");
		animator.SetBool("IsOpen", false);
	}
}