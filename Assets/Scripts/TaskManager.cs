using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private Transform npcs;
    [SerializeField] private Task[] tasks; // Görev listesini Inspector'da düzenlenebilir hale getirir
    private int currentTaskIndex; // Şu anki görev indeksi
    [SerializeField] private TextMeshProUGUI taskText; // Görev metnini göstermek için TextMeshPro bileşeni
    [SerializeField] private Dialogue dialogue; // Diyalog bileşeni referansı

    public void Awake()
    {
        dialogue.OnDialogueEnd += OnDialogueEnd; // Subscribe to the OnDialogueEnd event
    }

    private void OnDestroy()
    {
        dialogue.OnDialogueEnd -= OnDialogueEnd; // Unsubscribe from the OnDialogueEnd event
    }

    private void OnDialogueEnd(GameObject talkingObject)
    {
        TaskType taskType = GetCurrentTask().Type; // Görev türünü al
        if (taskType == TaskType.TalkToNPC) // Eğer görev türü TalkToNPC ise
        {
            if (talkingObject == GetCurrentTask().NPC) // Eğer konuşulan NPC şu anki görevdeki NPC ile aynıysa
            {
                GetCurrentTask().CompleteTask(); // Görevi tamamla
                SetTask(currentTaskIndex + 1); // Sonraki göreve geç
            }
        }
    }

    void Start()
    {
        SetTask(0); // İlk görevi ayarla
    }

    public void SetTask(int index)
    {
        if (index < 0 || index >= tasks.Length) return; // Geçerli bir indeks kontrolü
        currentTaskIndex = index; // Şu anki görev indeksini ayarla
        
        Task currentTask = GetCurrentTask(); // Şu anki görevi al
        if (currentTask == null) return; // Eğer görev yoksa çık
        
        if(currentTask.Type == TaskType.TalkToNPC) // Eğer görev türü TalkToNPC ise
        {
            foreach (Transform npc in npcs) // NPC'leri döngü ile gez
            {
                npc.GetComponent<NPC>().SetState(npc.gameObject == currentTask.NPC ? NPC.NPCState.Available : NPC.NPCState.Busy); // Eğer NPC şu anki görevdeki NPC ise Available, değilse Busy yap
            }
        }
        UpdateTaskText(); // Görev metnini güncelle
    }

    public Task GetCurrentTask()
    {
        if (currentTaskIndex < tasks.Length)
            return tasks[currentTaskIndex]; // Şu anki görevi döndür
        return null; // Tüm görevler tamamlandıysa null döndür
    }

    private void UpdateTaskText()
    {
        if (currentTaskIndex < tasks.Length)
        {
            Task currentTask = GetCurrentTask(); // Şu anki görevi al
            taskText.text = currentTask.Description; // Şu anki görevin açıklamasını göster
            
            if(currentTask.Type == TaskType.CollectItem) // Eğer görev türü CollectItem ise
            {
                taskText.text += " " + currentTask.Item.CollectedAmount + "/" + currentTask.Item.RequiredAmount; // Görev açıklamasına toplanan miktarı ekle
            }
        }
        else
        {
            taskText.text = "All tasks completed!"; // Tüm görevler tamamlandığında mesaj göster
        }
    }

    public void UpdateTaskItem(ItemType itemType, int amount) {
        Task currentTask = GetCurrentTask(); // Şu anki görevi al
        if (currentTask == null) return; // Eğer görev yoksa çık
        
        if (!(currentTask.Type == TaskType.CollectItem && currentTask.Item.ItemType == itemType)) // Eğer görev türü CollectItem ve görevdeki item türü ile eşleşiyorsa
            return;
        
        currentTask.Item.AddCollectedAmount(amount); // Görevdeki item miktarını güncelle
        UpdateTaskText(); // Görev metnini güncelle
        
        if (currentTask.Item.CollectedAmount >= currentTask.Item.RequiredAmount) // Eğer toplanan miktar gerekli miktarı aşıyorsa
        {
            currentTask.CompleteTask(); // Görevi tamamla
            SetTask(currentTaskIndex + 1); // Sonraki göreve geç
        }
        
    }
    

}
