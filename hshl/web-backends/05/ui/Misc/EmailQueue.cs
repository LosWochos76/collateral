using MimeKit;

namespace ToDoUI.Misc;

public class EmailQueue
{
    private Queue<MimeMessage> messages = new Queue<MimeMessage>();
    
    public void Enqueue(MimeMessage message)
    {
        lock (messages)
            messages.Enqueue(message);
    }

    public MimeMessage Dequeue()
    {
        lock (messages)
            return messages.Dequeue();
    }

    public bool HasMessages
    {
        get { lock (messages) return messages.Count > 0; } 
    }
}
