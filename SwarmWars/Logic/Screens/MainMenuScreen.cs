using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private MenuEntry menuEntryCampaign;
        private MenuEntry menuEntryHowToPlay;
        private MenuEntry menuEntryExit;

        public MainMenuScreen()
            : base()
        {
            menuEntryCampaign = new MenuEntry("Campaign");
            menuEntryCampaign.Index = 0;
            menuEntryHowToPlay = new MenuEntry("Arena");
            menuEntryHowToPlay.Index = 1;
            menuEntryExit = new MenuEntry("Exit");
            menuEntryExit.Index = 2;
            menuEntryCampaign.Selected += new EventHandler<EventArgs>(menuEntryCampaign_Selected);
            menuEntryHowToPlay.Selected += new EventHandler<EventArgs>(menuEntryHowToPlay_Selected);
            menuEntryExit.Selected += new EventHandler<EventArgs>(menuEntryExit_Selected);
            MenuEntries.Add(menuEntryCampaign);
            MenuEntries.Add(menuEntryHowToPlay);
            MenuEntries.Add(menuEntryExit);

            Resources.Player_Power = 1;
            Resources.Player_Regen = 1;
            Resources.Player_Speed = 1;
            Resources.Completed_Levels = 1;

           
        }

        void menuEntryHowToPlay_Selected(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new HowToPlayScreen());
            //ExitScreen();
        }

        

        void menuEntryCampaign_Selected(object sender, EventArgs e)
        {
            
            ScreenManager.AddScreen(new WorldScreen());
                
        }
        
        void menuEntryExit_Selected(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }
       
    }
}
