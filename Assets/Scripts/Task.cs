using System;
using UnityEngine;

    public enum TaskType
    {
        TalkToNPC,
        CollectItem
    }

    [Serializable] // Task sınıfını Inspector'da görünür hale getirir
    public class Task
    {
        [SerializeField] private TaskType type; // Görev türü
        [SerializeField] private string description; // Görev açıklaması
        [SerializeField] private bool isCompleted; // Görev tamamlandı mı?
        [SerializeField] private GameObject npc; // NPC referansı (sadece TalkToNPC için)
        [SerializeField] private TaskItem item; // Toplanacak nesne referansı (sadece CollectItem için)

        public TaskType Type => type; // Görev türünü almak için
        public string Description => description; // Görev açıklamasını almak için
        public GameObject NPC => npc; // NPC referansını almak için
        public TaskItem Item => item; // Toplanacak nesne referansını almak için
        public bool IsCompleted
        {
            get => isCompleted;
            set => isCompleted = value;
        }

        public void CompleteTask()
        {
            isCompleted = true;
        }
    }