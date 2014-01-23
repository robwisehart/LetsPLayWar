using LetsPlayWar.Contracts;
using LetsPlayWar.Entities;
using LetsPlayWar.Services;
using LetsPlayWar.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LetsPlayWar.WebUI.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        #region Private Members
        /// <summary>
        /// The game service instance.
        /// </summary>
        IGameService _game = null;
        /// <summary>
        /// Lock object for instantiating the game service.  This allows instantiation to be thread-safe.
        /// </summary>
        object _gameLock = new object();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HomeController()
        {
        }
        /// <summary>
        /// Testing constructor.
        /// </summary>
        /// <param name="gameService">Mock game service.</param>
        public HomeController(IGameService gameService)
        {
            _game = gameService;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Game Service.
        /// </summary>
        protected internal IGameService GameService
        {
            get
            {
                if (_game == null)
                {
                    lock(_gameLock)
                    {
                        if (_game == null)
                        {
                            _game = new GameService();
                        }
                    }
                }

                return _game;
            }
        }
        /// <summary>
        /// Gets/sets the current game in the session.
        /// </summary>
        protected internal Game CurrentGame
        {
            get
            {
                Game g = this.Session["CurrentGame"] as Game;

                return g;
            }
            set
            {
                this.Session["CurrentGame"] = value;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build a single string from the excption stack.
        /// </summary>
        /// <param name="startingMessage">The top-level message to display.</param>
        /// <param name="ex">The caught exception.</param>
        /// <returns>The string to display.</returns>
        private string BuildExceptionString(string startingMessage, Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(startingMessage);
            sb.Append("<br />");

            while (ex != null)
            {
                sb.Append(ex.Message);
                sb.Append("<br />");

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
        #endregion

        #region Actions
        /// <summary>
        /// GET: /Index/
        /// </summary>
        /// <returns>The index page.</returns>
        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// Begin a ne game.
        /// </summary>
        /// <param name="info">New game information model.</param>
        /// <returns>The run round page or the index page if info is missing.</returns>
        public ActionResult BeginGame(NewGameInfo info)
        {
            if (!this.ModelState.IsValid)
            {
                return View("Index");
            }

            try
            {
                Game g = this.GameService.StartGame(info.Player1Name, info.Player2Name);

                if (g == null)
                {
                    throw new Exception("g returned NULL.");
                }

                this.CurrentGame = g;
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = this.BuildExceptionString("There was an error starting the game.", ex);
                this.Redirect("Index");
            }

            return Redirect("RunRound");
        }
        /// <summary>
        /// Run a single round.
        /// </summary>
        /// <returns></returns>
        public ActionResult RunRound()
        {
            Game g = this.CurrentGame;

            switch (g.Status)
            {
                case GameStatus.Draw:
                case GameStatus.Player1Wins:
                case GameStatus.Player2Wins:
                    return Redirect("FinishGame");
                case GameStatus.NotStarted:
                case GameStatus.Unknown:
                    return Redirect("Index");
                case GameStatus.InProgress:
                    break;
            }

            GameRound gr = null;

            try
            {
                gr = this.GameService.RunRound(g);

                if (gr == null)
                {
                    throw new Exception("gr returned null.");
                }
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = this.BuildExceptionString("There was an error running this round of the game.", ex);
                this.Redirect("Index");
            }

            this.ViewData["Player1Name"] = g.Player1.Name;
            this.ViewData["Player2Name"] = g.Player2.Name;
            this.ViewData["GameStatus"] = g.Status;
            this.ViewData["GameLog"] = g.ToString();

            return View("GameRoundView", gr);
        }
        /// <summary>
        /// Show the game is complete screen.
        /// </summary>
        /// <returns>The finished game page.</returns>
        public ActionResult FinishGame()
        {
            Game g = this.CurrentGame;

            try
            {
                while (g.Status == GameStatus.InProgress)
                {
                    GameRound gr = this.GameService.RunRound(g);
                }
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = this.BuildExceptionString("There was an error finishing this game.", ex);
                this.Redirect("Index");
            }

            FinishedGameInfo info = new FinishedGameInfo();
            info.FinalGameStatus = g.Status;
            info.WinnerStatement = g.ToString();
            info.PathToGameLog = g.PathToGameLog;
            
            this.ViewData["Player1Name"] = g.Player1.Name;
            this.ViewData["Player2Name"] = g.Player2.Name;
            this.ViewData["GameStatus"] = g.Status;
            this.ViewData["GameLog"] = g.ToString();

            return View("FinishedGame",info);
        }
        #endregion

    }
}
