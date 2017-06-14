using System.Speech.Recognition;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Interactive
{
    public class Speech
    {
        private SpeechRecognitionEngine engine;
        private System.Windows.Controls.Label interactiveLabel;

        private string[] vocabulary = new string[] {
            "new task",
            "open",
            "modify",
            "clear",
            "save",
            "close",
            "delete",
            "up",
            "down",
            "left",
            "right",
            "assign to me",
            "label red",
            "label blue",
            "label green",
            "see sharp rocks",
            "demonstrate the future",
            "i love baba"
        };

        public Speech(System.Windows.Controls.Label interactiveLabel)
        {
            this.interactiveLabel = interactiveLabel;
        }

        public void Initialize()
        {
            if (engine == null)
            {
                var recognizer = SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault(r => r.Culture.Name.Equals("en-US"));
                engine = new SpeechRecognitionEngine(recognizer.Id);

                var grammer = new GrammarBuilder { Culture = recognizer.Culture };
                grammer.Append(new Choices(vocabulary));
                engine.LoadGrammar(new Grammar(grammer));
                //this.Speech.LoadGrammar(new DictationGrammar());

                engine.SetInputToDefaultAudioDevice();
                engine.RecognizeAsync(RecognizeMode.Multiple);
                engine.SpeechRecognized += speechRecognitionEngine_SpeechRecognized;
            }
        }

        public void Dispose()
        {
            if (engine != null)
            {
                engine.RecognizeAsyncStop();
                engine = null;
            }
        }

        private void speechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "new task":
                    SendKeys.SendWait("{DOWN}");
                    SendKeys.SendWait("n");
                    break;
                case "open":
                    SendKeys.SendWait("{ENTER}");
                    break;
                case "modify":
                    SendKeys.SendWait("e");
                    break;
                case "clear":
                    SendKeys.SendWait("^a");
                    SendKeys.SendWait("{BACKSPACE}");
                    break;
                case "save":
                    SendKeys.SendWait("^{ENTER}");
                    SendKeys.SendWait("{ESC}");
                    break;
                case "close":
                    SendKeys.SendWait("{ESC}");
                    break;
                case "delete":
                    SendKeys.SendWait("c");
                    break;
                case "up":
                    SendKeys.SendWait("{UP}");
                    break;
                case "down":
                    SendKeys.SendWait("{DOWN}");
                    break;
                case "left":
                    SendKeys.SendWait("{LEFT}");
                    break;
                case "right":
                    SendKeys.SendWait("{RIGHT}");
                    break;
                case "assign to me":
                    SendKeys.SendWait(" ");
                    break;
                case "label red":
                    SendKeys.SendWait("l");
                    Thread.Sleep(100);
                    SendKeys.SendWait("r");
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait("{ESC}");
                    break;
                case "label blue":
                    SendKeys.SendWait("l");
                    Thread.Sleep(100);
                    SendKeys.SendWait("b");
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait("{ESC}");
                    break;
                case "label green":
                    SendKeys.SendWait("l");
                    Thread.Sleep(100);
                    SendKeys.SendWait("g");
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait("{ESC}");
                    break;
                case "see sharp rocks":
                    SendKeys.SendWait("C# Rocks!!");
                    break;
                default: //"demonstrate the future"
                    SendKeys.SendWait(e.Result.Text);
                    break;
            }

            interactiveLabel.Content = e.Result.Text;
        }
    }
}
