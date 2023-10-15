using System;

using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Containers;

using TDL.Game.Graphics.UI;
using System.Collections.Generic;
using TDL.Game.Base;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace TDL.Game.Graphics.Screens
{
    public partial class TDLMainScreen : TDLScreen
    {
        private Container _toolsContainer;
        private Container _currentContainer;

        public List<TDLTask> tasks = new List<TDLTask>();
        private TDLTask _currentTask; 
        private TDLTextBox _nameTextBox;
        private TDLBasicText _descText;
        private TDLTextBox _descTextBox;
        private TDLBasicText _noneText;


        private TDLButton _closeTaskButton;
        private TDLButton _saveTaskButton;
        private TDLBasicText _taskTemplateText;
        private TDLBasicText _taskNameText;
        private TDLBasicText _taskDescText;
        private TDLBasicText _taskPriorityText;
        private TDLBasicText _taskStatusText;


        private TDLEnumDrowDown<TDLPriority> _priorityEnumDrowDown;
        private TDLEnumDrowDown<TDLStatusTask> _statusEnumDrowDown;
        private BasicScrollContainer _scrollContainer;
        private FillFlowContainer _scrollflowContainer;

        public TDLMainScreen()
        {
            AddInternal(new TDLBackground("bg", new Vector2(12.0f)));

            //AddInternal(new Box
            //{ 
            //    Colour = new Color4(34, 30, 30, 255),
            //    RelativeSizeAxes = Axes.Both,
            //    Anchor = Anchor.Centre,
            //    Origin = Anchor.Centre,
            //    FillMode = FillMode.Fill,
            //});

            AddInternal(new FillFlowContainer
            {
                Direction = FillDirection.Horizontal,
                RelativeSizeAxes = Axes.Both,

                Children = new Drawable[]
                {
                    // Add menu 
                    _toolsContainer = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        CornerRadius = 10,
                        RelativeSizeAxes = Axes.Y,
                        Width = 300,
                        Masking = true,
                        Margin = new MarginPadding{ Left = 60 },
                        //Position = new Vector2(60.0f, 0.0f),
                        Scale = new Vector2(0.9f, 0.9f),
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                Colour = new Color4(41, 41, 41, 255),
                                RelativeSizeAxes = Axes.Both,
                                Depth = float.MaxValue,

                            },
                            new BasicScrollContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                Depth = -1,
                                ScrollbarOverlapsContent = false,
                                Child = new FillFlowContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Vertical,
                                    Children = new Drawable[]
                                    {

                                        new TDLBasicText
                                        {
                                            Text = "Create Task",
                                            //RelativeSizeAxes = Axes.Both,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(2.0f, 2.0f),
                                        },
                                        new TDLBasicText
                                        {
                                            Text = "Name:",
                                            //RelativeSizeAxes = Axes.Both,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(1.0f, 1.0f),
                                        },
                                        _nameTextBox = new TDLTextBox
                                        {
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Height = 30,
                                            Width = 250,
                                            //LengthLimit = 20,
                                            Depth = -1,
                                        },

                                        _descText = new TDLBasicText
                                        {
                                            Text = "Description:",
                                            //RelativeSizeAxes = Axes.Both,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(1.0f, 1.0f),
                                        },
                                        _descTextBox = new TDLTextBox
                                        {
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Height = 30,
                                            Width = 250,
                                            //LengthLimit = 20,
                                            Depth = -1,
                                        },
                                        new TDLBasicText
                                        {
                                            Text = "Priority:",
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(1.0f, 1.0f),
                                        },
                                        _priorityEnumDrowDown = new TDLEnumDrowDown<TDLPriority>
                                        {
                                            //RelativeSizeAxes = Axes.Both,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(1.2f, 1.2f),
                                            Width = 200,

                                        },
                                        // new TDLBasicText
                                        // {
                                        //     Text = "Status:",
                                        //     Anchor = Anchor.TopLeft,
                                        //     Origin = Anchor.TopLeft,
                                        //     Margin = new MarginPadding(10),
                                        //     Colour = new Color4(200, 200, 200, 255),
                                        //     Depth = -1,
                                        //     Scale = new Vector2(1.0f, 1.0f),
                                        // },
                                        // _statusEnumDrowDown = new TDLEnumDrowDown<TDLStatusTask>
                                        // {
                                        //     //RelativeSizeAxes = Axes.Both,
                                        //     Anchor = Anchor.TopLeft,
                                        //     Origin = Anchor.TopLeft,
                                        //     Margin = new MarginPadding(10),
                                        //     Colour = new Color4(200, 200, 200, 255),
                                        //     Depth = -1,
                                        //     Scale = new Vector2(1.2f, 1.2f),
                                        //     Width = 200,
                                        // 
                                        // },
                                        new TDLButton
                                        {
                                              Anchor = Anchor.TopLeft,
                                              Origin = Anchor.TopLeft,
                                              Margin = new MarginPadding(10),
                                              Size = new Vector2(60, 65),
                                              Text = "Create",
                                              onButtonClick = CreateTask,
                                        },
                                        new TDLBasicText
                                        {
                                            Text = "Tasks:",
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),
                                            Colour = new Color4(200, 200, 200, 255),
                                            Depth = -1,
                                            Scale = new Vector2(1.0f, 1.0f),
                                        },


                                        new TDLButton
                                        {
                                              Anchor = Anchor.TopLeft,
                                              Origin = Anchor.TopLeft,
                                              Margin = new MarginPadding { Left = 10 },
                                              Size = new Vector2(60, 65),
                                              Text = "Clear",
                                              onButtonClick = ClearTasks,
                                        },
                                        new Container
                                        {
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                            Margin = new MarginPadding(10),

                                            Children = new Drawable[]
                                            {
                                                new Box
                                                {

                                                    Depth = -1,
                                                    Height = 150,
                                                    Width = 250,
                                                    Colour = new Color4(80, 80, 80, 255),

                                                },
                                                _scrollContainer = new BasicScrollContainer
                                                {
                                                    Depth = -1,
                                                    Height = 150,
                                                    Width = 250,
                                                    ScrollbarOverlapsContent = false,
                                                     Child = _scrollflowContainer =  new FillFlowContainer
                                                     {
                                                         RelativeSizeAxes = Axes.X,
                                                         AutoSizeAxes = Axes.Y,
                                                         Direction = FillDirection.Vertical
                                                     }
                                                },
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    },

                    _currentContainer = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        CornerRadius = 10,
                        //RelativeSizeAxes = Axes.Both,
                        Width = 1200,
                        Height = 800,
                        Masking = true,
                        //Position = new Vector2(160.0f, 0.0f),
                        Scale = new Vector2(0.6f, 0.6f),
                        Margin = new MarginPadding { Left = 60 },

                        Children = new Drawable[]
                        {
                            _taskTemplateText = new TDLBasicText
                            {
                                Text = "Current Task",
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Margin = new MarginPadding{ Bottom = 10 },
                                Colour = new Color4(200, 200, 200, 255),
                                Depth = -1,
                                Position = new Vector2(0.0f, 0.0f),
                                Scale = new Vector2(3.0f, 3.0f),
                            },
                            _noneText = new TDLBasicText
                            {
                                Text = "None",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = new Color4(200, 0, 0, 255),
                                Depth = -1,
                                Margin = new MarginPadding{ Bottom = 10 },
                                Scale = new Vector2(3.0f, 3.0f),
                            },
                            new FillFlowContainer
                            {
                                Direction = FillDirection.Horizontal,
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    _closeTaskButton = new TDLButton
                                    {
                                          Anchor = Anchor.BottomRight,
                                          Origin = Anchor.BottomRight,
                                          Margin = new MarginPadding { Left = 10, Top = 10 },
                                          Size = new Vector2(100, 75),
                                          Scale = new Vector2(2,2),
                                          Text = "Close task",
                                          onButtonClick = CloseTask,
                                    },
                                    _saveTaskButton = new TDLButton
                                    {
                                          Anchor = Anchor.BottomRight,
                                          Origin = Anchor.BottomRight,
                                          Margin = new MarginPadding { Left = 50, Top = 10},
                                          Size = new Vector2(100, 75),
                                          Scale = new Vector2(2,2),
                                          Text = "Save to",
                                          onButtonClick = OpenFileSelector,
                                    },
                                }
                            },
                            new FillFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Vertical,
                                Position = new Vector2 { Y = 100.0f },
                                Children = new Drawable[]
                                {
                                    _taskNameText = new TDLBasicText
                                    {
                                        Text = "Name:",
                                        Anchor = Anchor.TopLeft,
                                        Origin = Anchor.TopLeft,
                                        Colour = new Color4(200, 200, 200, 255),
                                        Depth = -1,
                                        Margin = new MarginPadding{ Bottom = 30 },
                                        Scale = new Vector2(1.5f, 1.5f),
                                    },
                                    _taskPriorityText = new TDLBasicText
                                    {
                                        Text = "Priority:",
                                        Anchor = Anchor.TopLeft,
                                        Origin = Anchor.TopLeft,
                                        Colour = new Color4(200, 200, 200, 255),
                                        Depth = -1,
                                        Margin = new MarginPadding { Bottom = 30 },
                                        Scale = new Vector2(1.5f, 1.5f),
                                    },
                                    _taskDescText = new TDLBasicText
                                    {
                                        Text = "Description:",
                                        Anchor = Anchor.TopLeft,
                                        Origin = Anchor.TopLeft,
                                        Colour = new Color4(200, 200, 200, 255),
                                        Depth = -1,
                                        Margin = new MarginPadding { Bottom = 30 },
                                        Scale = new Vector2(1.5f, 1.5f),
                                    },
                                    _taskStatusText = new TDLBasicText
                                    {
                                        Text = "Status:",
                                        Anchor = Anchor.TopLeft,
                                        Origin = Anchor.TopLeft,
                                        Colour = new Color4(200, 200, 200, 255),
                                        Depth = -1,
                                        Margin = new MarginPadding { Bottom = 30 },
                                        Scale = new Vector2(1.5f, 1.5f),
                                    },
                                }
                            },
                            new Box
                            {
                                Colour = new Color4(41, 41, 41, 255),
                                Width = 1200,
                                Height = 800,
                                Depth = float.MaxValue,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                //Margin = new MarginPadding { Left = 400 },
                            },
                        }
                    }

                }
            });


            PrepareUITaskNotActive();

            LoadTasksFromJson(string.Empty);
        }

        private BasicFileSelector _fileSelector;
        private FillFlowContainer _filefillFlow;
        private TDLTextBox _fileTextBox;

        void SaveTo()
        {
            if(_fileTextBox.Text == string.Empty)
            {
                _fileTextBox.CallNotifyInputError();
                return;
            }

            var path = _fileSelector.CurrentPath.Value.FullName + "\\" + _fileTextBox.Text + ".json";
            SaveTasksToJson(path);

            CloseFileSelector();
        }

        void CloseFileSelector()
        {
            _currentContainer.Remove(_filefillFlow, true);
            PrepareUITaskActive();
        }

        void OpenFileSelector()
        {
            var _fileSelectorContainer = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.X,
                Height = 600,
                Children = new Drawable[]
                {
                    _fileSelector = new BasicFileSelector
                    {
                        //Height = 500,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                }
            };

            var _fileToolsContainer = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.X,
                Height = 200,
                Children = new Drawable[]
                {
                    new FillFlowContainer
                    {
                        Direction = FillDirection.Horizontal,
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new TDLButton
                            {
                                  Anchor = Anchor.BottomLeft,
                                  Origin = Anchor.BottomLeft,
                                  Margin = new MarginPadding { Left = 10, Top = 10},
                                  Size = new Vector2(100, 75),
                                  Scale = new Vector2(2,2),
                                  Text = "Save",
                                  onButtonClick = SaveTo,
                            },
                            new TDLButton
                            {
                                  Anchor = Anchor.BottomLeft,
                                  Origin = Anchor.BottomLeft,
                                  Margin = new MarginPadding { Left = 10, Top = 10},
                                  Size = new Vector2(100, 75),
                                  Scale = new Vector2(2,2),
                                  Text = "Close",
                                  onButtonClick = CloseFileSelector,
                            },
                            new TDLBasicText
                            {
                                Text = "File Name:",
                                Anchor = Anchor.BottomLeft,
                                Origin = Anchor.BottomLeft,
                                Colour = new Color4(200, 200, 200, 255),
                                Depth = -1,
                                Margin = new MarginPadding { Left = 10, Top = 50},
                                Scale = new Vector2(2.0f, 2.0f),
                            },
                             _fileTextBox = new TDLTextBox
                             {
                                 Anchor = Anchor.BottomLeft,
                                 Origin = Anchor.BottomLeft,
                                 Margin = new MarginPadding { Left = 10, Top = 30},
                                 Height = 75,
                                 Width = 500,
                                 //LengthLimit = 20,
                                 Depth = -1,
                             },
                        }
                    }
                }
            };

            _currentContainer.Add(
                _filefillFlow = new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                       _fileSelectorContainer, _fileToolsContainer
                    }
                }
            );
            PrepareUIHideAll();

        }

        void ClearTasks()
        {
            tasks.Clear();
            _scrollflowContainer.Clear();
            PrepareUITaskNotActive();
        }

        void PrepareUITaskActive()
        {
            _noneText.Hide();

            _taskTemplateText.Show();
            _taskNameText.Show();
            _taskPriorityText.Show();
            _taskDescText.Show();
            _taskStatusText.Show();
            _closeTaskButton.Show();
            _saveTaskButton.Show();
        }

        void PrepareUITaskNotActive()
        {
            _noneText.Show();
            _taskTemplateText.Show();

            _taskNameText.Hide();
            _taskPriorityText.Hide();
            _taskDescText.Hide();
            _taskStatusText.Hide();
            _closeTaskButton.Hide();
            _saveTaskButton.Hide();
        }

        void PrepareUIHideAll()
        {
            _noneText.Hide();

            _taskTemplateText.Hide();
            _taskNameText.Hide();
            _taskPriorityText.Hide();
            _taskDescText.Hide();
            _taskStatusText.Hide();
            _closeTaskButton.Hide();
            _saveTaskButton.Hide();
        }

        void SetTaskAsCurrent(TDLTask task)
        {
            if (task == null)
            {
                Logger.Log("Can't set task because it is null", level: LogLevel.Error);
                return;
            }

            _currentTask = task;

            _taskNameText.Text = $"Name: {task.name}";
            _taskPriorityText.Text = $"Priority: {task.priority}";
            _taskStatusText.Text = $"Status: {task.status}";

            if(task.description == string.Empty)
            {
                _taskDescText.Text = $"Description: None";
            }
            else
            {
                _taskDescText.Text = $"Description: {task.description}";
            }

            PrepareUITaskActive();
        }

        void LoadTasksFromJson(string path)
        {
            var final_path = path == string.Empty ? @"..\tasks.json" : path;
            var data = File.ReadAllText(final_path);
            tasks = JsonConvert.DeserializeObject<List<TDLTask>>(data);

            // Clear previous if we are had something
            _scrollflowContainer.Clear();

            LoadTasks();
        }

        void SaveTasksToJson(string path)
        {
            var final_path = path == string.Empty ? @"..\tasks.json" : path;
            Logger.Log($"Save file {final_path}", level: LogLevel.Debug);
            var json = JsonConvert.SerializeObject(tasks.ToArray());            
            File.WriteAllText(final_path, json);            
        }

        void CloseTask()
        {
            PrepareUITaskNotActive();

            // FIX ME: very dirty way to clear something from scrollContainer 
            foreach (var child in _scrollflowContainer.Children)
            {
                TDLTaskButton button = child as TDLTaskButton;

                if (button.Task == _currentTask)
                {
                    _scrollflowContainer.Remove(child, true);
                    break;
                }
            }

            tasks.Remove(_currentTask);

            SaveTasksToJson(string.Empty);
        }
        void LoadTasks()
        {
            foreach(var task in tasks)
            {
                _scrollflowContainer.Add(new TDLTaskButton
                {
                    Text = $"{task.name}",
                    Task = task,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(200, 20),
                    onTaskButtonClick = SetTaskAsCurrent,
                    //Margin = new MarginPadding { Bottom = 10 },
                    //Position = new Vector2(0.0f, (52.0f * (tasks.Count - 1))),
                    Margin = new MarginPadding { Bottom = 5 },
                    Scale = new Vector2(1.2f),
                });
            }
        }

        void CreateTask()
        {
            if(_nameTextBox.Text == string.Empty)
            {
                _nameTextBox.CallNotifyInputError();
                return;
            }

            TDLTask task = new TDLTask();
            task.name        = _nameTextBox.Text;
            task.description = _descTextBox.Text;
            task.priority = _priorityEnumDrowDown.Current.Value;
            task.status = TDLStatusTask.InProgress;

            tasks.Add(task);
            _nameTextBox.Text = default;
            _descTextBox.Text = default;

            // TODO: Resort by priority 

            _scrollflowContainer.Add(new TDLTaskButton
            { 
                Text = $"{task.name}",
                Task = task,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Size = new Vector2(200, 20),
                onTaskButtonClick = SetTaskAsCurrent,
                //Margin = new MarginPadding { Bottom = 10 },
                //Position = new Vector2(0.0f, (52.0f * (tasks.Count - 1))),
                Margin = new MarginPadding { Bottom = 5 },
                Scale = new Vector2(1.2f),
            });

            SaveTasksToJson(string.Empty);
        }
    }
}
