namespace Domain.Services.Message;

public class MessageService : IMessageService
{
    public MessageService()
    {
    }

    public string GetMessage(List<List<string>> messages)
    {
        List<string> message = new List<string>();
        string msg = string.Empty;
        foreach (var item in messages.OrderBy(m => m.Count))
        {
            if (message.Count == 0)
            {
                message.AddRange(item);
            }
            else
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (!string.IsNullOrEmpty(item[i]))
                    {
                        if (i >= message.Count)
                        {
                            message.Add(item[i]);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(message[i]))
                            {
                                message[i] = item[i];
                            }
                        }
                    }
                }
            }
        }

        var newWords = ValidateMessage(message);
        if (newWords.Count < 3)
        {
            throw new GeneralException("no hay suficiente información");
        }

        foreach (var item in newWords)
        {
            msg += string.Concat(item, " ");
        }

        return msg;
    }

    public string GetMessage2(List<List<string>> messages)
    {
        List<string> message = new List<string>();
        string mesagge = string.Empty;
        int countMax = messages.Max(p => p.Count);
        int cointmin = messages.Min(p => p.Count);

        if (countMax == cointmin)
        {
            for (int i = 0; i < countMax; i++)
            {
                foreach (var item in messages)
                {
                    if (!string.IsNullOrEmpty(item[i]))
                    {
                        message.Add(item[i]);
                        break;
                    }
                }
            }

            if (message.Count != countMax)
            {
                throw new GeneralException("no hay suficiente información");
            }

            var newWords = ValidateMessage(message);
            foreach (var item in newWords)
            {
                mesagge += string.Concat(item, " ");
            }
        }

        return mesagge.Trim();
    }

    private List<string> ValidateMessage(List<string> words)
    {
        List<string> newWords = new List<string>();
        for (int i = 0; i < words.Count; i++)
        {
            string word = words[i];
            string lastword = newWords.Count == 0 ? string.Empty : newWords[newWords.Count - 1];
            if (i == 0)
            {
                newWords.Add(word);
            }
            else if (word != lastword)
            {
                newWords.Add(word);
            }
        }

        return newWords;
    }
}

