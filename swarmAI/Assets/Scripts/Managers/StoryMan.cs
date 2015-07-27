using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryMan {

	public List<StoryElem> storyList;
	public Text title;
	public Text content;
	public Text subContent;


	public void Init(GameObject parent) {
		storyList = new List<StoryElem>();
		foreach (Text tmp in parent.GetComponentsInChildren<Text>()) {
			if (tmp.name == "Title") {
				title = tmp;
				title.text = "";
				continue;
			}
			if (tmp.name == "Content") {
				content = tmp;
				content.text = "";
				continue;
			}
			if (tmp.name == "SubContent") {
				subContent = tmp;
				subContent.text = "";
				continue;
			}


		}
	}

	public void AddStory(StoryElem storyElem) {
		if (storyElem.title != "N/A") title.text = storyElem.title;
		if (storyElem.content != "N/A") content.text = storyElem.content;
		if (storyElem.subContent != "N/A") subContent.text = storyElem.subContent;
		storyList.Add(storyElem);
	}

	public void Update(float timeElasped) {
		while (storyList.Count != 0 && storyList[0].endTime < timeElasped) {
			if (storyList[0].title == title.text) title.text = "";
			if (storyList[0].content == content.text) content.text = "";
			if (storyList[0].subContent == subContent.text) subContent.text = "";
			storyList.RemoveAt(0);
		}
	}


}

public struct StoryElem {

	public float endTime {get;private set;}
	public string title {get;private set;}
	public string content {get;private set;}
	public string subContent {get;private set;}

	public StoryElem(JSONNode curEvent) {
		endTime = curEvent["life"].AsFloat + curEvent["time"].AsFloat;
		if (curEvent["title"].ToString() != null) {
			title = curEvent["title"].ToString();
		}
		else {
			title = "N/A";
		}
		if (curEvent["content"].ToString() != null) {
			content = curEvent["content"].ToString();
		}
		else {
			content = "N/A";
		}
		if (curEvent["subContent"].ToString() != null) {
			subContent = curEvent["subContent"].ToString();
		}
		else {
			subContent = "N/A";
		}
	}

}