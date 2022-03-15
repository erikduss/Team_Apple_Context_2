using System.Collections.Generic;

public interface IClickable
{
    void ObjectClicked();
}

public interface IQuest
{
    int questID { get; set; }

    string questName { get; set; }

    QuestStatus questStatus { get; set; }

    int currentQuestStep { get; set; }

    int amountOfQuestSteps { get; set; }

    List<QuestStep> questSteps { get; set; }

    void QuestCompleted();

    void CheckForNextSteps();

    string GetCurrentTitle();

    string GetCurrentDialog();
}
