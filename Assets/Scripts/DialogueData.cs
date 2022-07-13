    using UnityEngine;

    [CreateAssetMenu(order = 1, menuName = "Dialogue Data", fileName = "Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public string[] lines;
        public Transform target;

        private TalkInteraction interaction;

        public string GetLine(int line)
        {
            if (line < lines.Length) 
                return lines[line];
            return "";  // out of lines
        }

        public void Interact(TalkInteraction interaction)
        {
            this.interaction = interaction;
            target = interaction.Target;
        }

        public void Finish()
        {
            var inter = interaction;
            interaction = null;
            inter.End();
        }
    }