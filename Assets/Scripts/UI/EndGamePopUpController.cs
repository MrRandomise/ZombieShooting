using System;
using System.Threading.Tasks;
using CharacterModel;
using UnityEngine;


namespace UI
{
    public sealed class EndGamePopUpController :IDisposable
    {
        private const float PopUpTimeout = 3f;
        private readonly Character _character;
        private readonly EndGamePopUpView endGamePopUp;

        public EndGamePopUpController(Character character,EndGamePopUpView endGamePopUp)
        {
            _character = character;
            this.endGamePopUp = endGamePopUp;
            _character.OnDeath.Subscribe(EndGame);
        }

        private void EndGame()
        {
            EndGameTask();
        }

        private async void EndGameTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(PopUpTimeout));
            endGamePopUp.ActivateObject();
            Time.timeScale = 0;
        }
        

        public void Dispose()
        {
            _character.OnDeath.UnSubscribe(EndGame);
        }
    }
}
