using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using BF1_Hack.GameStruct;
using GameHackFramework.Code;
using GameHackFramework.Code.Form;
using GameHackFramework.Code.GameHack.ESP;
using GameHackFramework.Code.GameHack.Misc;
using GameHackFramework.Code.GameHack.Radar;
using SharpDX;
using Color = SharpDX.Color;

namespace BF1_Hack
{
    public class HackManager : GameHackFramework.Code.HackManager
    {
        private readonly LocalPlayer _localPlayer = new LocalPlayer();
        private readonly List<Player> _players = new List<Player>();

        public HackManager()
        {
            RenderSurface = new RenderSurface();
            RenderSurface.Shown += (sender, args) => new Thread(Loop).Start();

            Render2D = new Render2D(RenderSurface.Handle);
            ProcessMemory = new ProcessMemory();
        }

        protected override void WriteMemory()
        {
            //long pGameContext = ProcessMemory.ReadInt64(Offsets.GameContext.GetInstance());

            //if (ProcessMemory.IsValid(pGameContext))
            //{
            //    long pPlayerManager = ProcessMemory.ReadInt64(pGameContext + Offsets.GameContext.PPlayerManager);

            //    if (ProcessMemory.IsValid(pPlayerManager))
            //    {
            //        long pLocalPlayer = ProcessMemory.ReadInt64(pPlayerManager + Offsets.ClientPlayerManager.PLocalPlayer);

            //        if (ProcessMemory.IsValid(pLocalPlayer))
            //        {
            //            long pClientSoldirEntity = ProcessMemory.ReadInt64(pLocalPlayer + Offsets.ClientPlayer.PControlledControllable);

            //            if (ProcessMemory.IsValid(pClientSoldirEntity))
            //            {
            //                long pClientSoldierWeapon = ProcessMemory.ReadInt64(pClientSoldirEntity + Offsets.ClientSoldierEntity.PClientSoldierWeapon);

            //                if (ProcessMemory.IsValid(pClientSoldierWeapon))
            //                {
            //                    long pClientWeapon = ProcessMemory.ReadInt64(pClientSoldierWeapon + Offsets.ClientSoldierWeapon.PClientWeapon);

            //                    if (ProcessMemory.IsValid(pClientWeapon))
            //                    {
            //                        long pWeaponFiringData = ProcessMemory.ReadInt64(pClientWeapon + Offsets.ClientWeapon.PWeaponFiringData);

            //                        if (ProcessMemory.IsValid(pWeaponFiringData))
            //                        {
            //                            long pGunSwayData = ProcessMemory.ReadInt64(pWeaponFiringData + Offsets.WeaponFiringData.PGunSwayData);

            //                            if (ProcessMemory.IsValid(pGunSwayData))
            //                            {
            //                                //no recoil
            //                                ProcessMemory.WriteFloat(pGunSwayData + Offsets.GunSwayData.Pitch, 0F);
            //                                ProcessMemory.WriteFloat(pGunSwayData + Offsets.GunSwayData.Yaw, 100F);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //if (ProcessMemory.IsValid(pGameContext))
            //{
            //    long pPlayerManager = ProcessMemory.ReadInt64(pGameContext + Offsets.GameContext.PPlayerManager);

            //    if (ProcessMemory.IsValid(pPlayerManager))
            //    {
            //        long pLocalPlayer = ProcessMemory.ReadInt64(pPlayerManager + Offsets.ClientPlayerManager.PLocalPlayer);

            //        if (ProcessMemory.IsValid(pLocalPlayer))
            //        {
            //            long pClientSoldirEntity = ProcessMemory.ReadInt64(pLocalPlayer + Offsets.ClientPlayer.PControlledControllable);

            //            if (ProcessMemory.IsValid(pClientSoldirEntity))
            //            {
            //                long address = ProcessMemory.ReadInt64(pClientSoldirEntity + 0x648);

            //                if (ProcessMemory.IsValid(address))
            //                {
            //                    address = ProcessMemory.ReadInt64(address + 0x900);

            //                    if (ProcessMemory.IsValid(address))
            //                    {
            //                        address = ProcessMemory.ReadInt64(address + 0x38);

            //                        if (ProcessMemory.IsValid(address))
            //                        {
            //                            address = ProcessMemory.ReadInt64(address + 0x30);

            //                            if (ProcessMemory.IsValid(address))
            //                            {
            //                                address = ProcessMemory.ReadInt64(address + 0x90);

            //                                if (ProcessMemory.IsValid(address))
            //                                {
            //                                    address = ProcessMemory.ReadInt64(address + 0x10);

            //                                    if (ProcessMemory.IsValid(address))
            //                                    {
            //                                        address = ProcessMemory.ReadInt64(address + 0xc0);

            //                                        if (ProcessMemory.IsValid(address))
            //                                        {
            //                                            ProcessMemory.WriteFloat(address + 0x140, 0); //bullet gravity
            //                                            ProcessMemory.WriteFloat(address + 0x168, 1000); //bullet start damage
            //                                            ProcessMemory.WriteFloat(address + 0x16c, 1000); //bullet end damage
            //                                            ProcessMemory.WriteByte(address + 0x18e, 1); //instant bullet
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        protected override void ReadMemory()
        {
            long pGameContext = ProcessMemory.ReadInt64(Offsets.GameContext.GetInstance());
            long pGameRenderer = ProcessMemory.ReadInt64(Offsets.GameRenderer.GetInstance());

            //read local player
            if (ProcessMemory.IsValid(pGameRenderer))
            {
                long pRenderView = ProcessMemory.ReadInt64(pGameRenderer + Offsets.GameRenderer.PRenderView);

                if (ProcessMemory.IsValid(pRenderView))
                {
                    _localPlayer.ViewProj = ProcessMemory.ReadMatrix(pRenderView + Offsets.RenderView.PViewProj);
                }
            }

            if (ProcessMemory.IsValid(pGameContext))
            {
                long pPlayerManager = ProcessMemory.ReadInt64(pGameContext + Offsets.GameContext.PPlayerManager);

                if (ProcessMemory.IsValid(pPlayerManager))
                {
                    long pLocalPlayer = ProcessMemory.ReadInt64(pPlayerManager + Offsets.ClientPlayerManager.PLocalPlayer);

                    if (ProcessMemory.IsValid(pLocalPlayer))
                    {
                        _localPlayer.Name = ProcessMemory.ReadAsciiString(pLocalPlayer + Offsets.ClientPlayer.Name, 10);
                        _localPlayer.TeamId = ProcessMemory.ReadInt32(pLocalPlayer + Offsets.ClientPlayer.TeamId);

                        long pSoldierEntity = ProcessMemory.ReadInt64(pLocalPlayer + Offsets.ClientPlayer.PControlledControllable);

                        if (ProcessMemory.IsValid(pSoldierEntity))
                        {
                            _localPlayer.Yaw = ProcessMemory.ReadFloat(pSoldierEntity + Offsets.ClientSoldierEntity.Yaw);

                            long pPredictedController = ProcessMemory.ReadInt64(pSoldierEntity + Offsets.ClientSoldierEntity.PPredictedController);

                            if (ProcessMemory.IsValid(pPredictedController))
                            {
                                _localPlayer.Position = ProcessMemory.ReadVector3(pPredictedController + Offsets.ClientSoldierPrediction.PPosition);
                            }
                        }
                    }
                }
            }

            //read players
            _players.Clear();

            if (ProcessMemory.IsValid(pGameContext))
            {
                long pPlayerManager = ProcessMemory.ReadInt64(pGameContext + Offsets.GameContext.PPlayerManager);

                if (ProcessMemory.IsValid(pPlayerManager))
                {
                    long ppPlayer = ProcessMemory.ReadInt64(pPlayerManager + Offsets.ClientPlayerManager.PClientPlayer);

                    if (ProcessMemory.IsValid(ppPlayer))
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            long pPlayer = ProcessMemory.ReadInt64(ppPlayer + i * 0x8);

                            if (ProcessMemory.IsValid(pPlayer))
                            {
                                Player player = new Player();

                                player.Name = ProcessMemory.ReadAsciiString(pPlayer + Offsets.ClientPlayer.Name, 10);
                                player.TeamId = ProcessMemory.ReadInt32(pPlayer + Offsets.ClientPlayer.TeamId);
                                player.IsSpectator = ProcessMemory.ReadBool(pPlayer + Offsets.ClientPlayer.IsSpectator);

                                long pSoldierEntity = ProcessMemory.ReadInt64(pPlayer + Offsets.ClientPlayer.PControlledControllable);

                                if (ProcessMemory.IsValid(pSoldierEntity))
                                {
                                    player.Yaw = ProcessMemory.ReadFloat(pSoldierEntity + Offsets.ClientSoldierEntity.Yaw);
                                    player.IsVisible = ProcessMemory.ReadBool(pSoldierEntity + Offsets.ClientSoldierEntity.IsOccluded);
                                    player.PoseType = (Player.Pose)ProcessMemory.ReadInt32(pSoldierEntity + Offsets.ClientSoldierEntity.PoseType);

                                    long pHealthComponent = ProcessMemory.ReadInt64(pSoldierEntity + Offsets.ClientSoldierEntity.PHealthComponent);

                                    if (ProcessMemory.IsValid(pHealthComponent))
                                    {
                                        player.Health = ProcessMemory.ReadFloat(pHealthComponent + Offsets.HealthComponent.Health);
                                        player.MaxHealth = ProcessMemory.ReadFloat(pHealthComponent + Offsets.HealthComponent.MaxHealth);
                                    }

                                    long pPredictedController = ProcessMemory.ReadInt64(pSoldierEntity + Offsets.ClientSoldierEntity.PPredictedController);

                                    if (ProcessMemory.IsValid(pPredictedController))
                                    {
                                        player.Position = ProcessMemory.ReadVector3(pPredictedController + Offsets.ClientPredictedController.Position);
                                    }
                                }

                                _players.Add(player);
                            }
                        }
                    }
                }
            }
        }

        protected override void Draw()
        {
            if (Radar2D.Option.IsVisible)
                Radar2D.Draw(Render2D, RenderSurface.Size);

            foreach (Player player in _players)
            {
                if (player.IsValid() && player.Name != _localPlayer.Name)
                {
                    if (Name.Option.IsVisible)
                    {
                        if (!Name.Option.IsOnlyEnemyVisible || Name.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            Name.Draw_y(Render2D, player.Position, _localPlayer.ViewProj, new Size(1920, 1080), player.Name, player.Height, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }

                    if (Health.Option.IsVisible)
                    {
                        if (!Health.Option.IsOnlyEnemyVisible || Health.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            Health.Draw_y(Render2D, player.Position, _localPlayer.ViewProj, new Size(1920, 1080), player.Health, player.MaxHealth, player.Height, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }

                    if (Box2D.Option.IsVisible)
                    {
                        if (!Box2D.Option.IsOnlyEnemyVisible || Box2D.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            Box2D.Draw_y(Render2D, new Size(1920, 1080), _localPlayer.ViewProj, player.Position, player.Height, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }

                    if (Box3D.Option.IsVisible)
                    {
                        if (!Box3D.Option.IsOnlyEnemyVisible || Box3D.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            Box3D.Draw_y(Render2D, player.BoundingBox(), player.Position, player.Yaw, _localPlayer.ViewProj, new Size(1920, 1080), player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }

                    if (SnapLine.Option.IsVisible)
                    {
                        if (!SnapLine.Option.IsOnlyEnemyVisible || SnapLine.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            SnapLine.Draw_y(Render2D, player.Position, _localPlayer.ViewProj, RenderSurface.Size, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }

                    if (Radar2D.Option.IsVisible)
                    {
                        if (!Radar2D.Option.IsOnlyEnemyVisible || Radar2D.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                        {
                            Radar2D.DrawPlayer(Render2D, player.Position, _localPlayer.Position, RenderSurface.Size, _localPlayer.Yaw, player.IsVisible, _localPlayer.TeamId == player.TeamId);

                            if (Radar2D.Option.IsVisibleName)
                                Radar2D.DrawName(Render2D, player.Position, _localPlayer.Position, RenderSurface.Size, player.Name, _localPlayer.Yaw, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                        }
                    }

                    if (CrosshairRadar.Option.IsVisible)
                    {
                        if (!CrosshairRadar.Option.IsOnlyEnemyVisible || CrosshairRadar.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                        {
                            CrosshairRadar.DrawPlayer(Render2D, player.Position, _localPlayer.Position, RenderSurface.Size, _localPlayer.Yaw, player.IsVisible, _localPlayer.TeamId == player.TeamId);

                            if (CrosshairRadar.Option.IsVisibleName)
                                CrosshairRadar.DrawName(Render2D, player.Position, _localPlayer.Position, RenderSurface.Size, player.Name, _localPlayer.Yaw, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                        }
                    }

                    if (DisplayRadar.Option.IsVisible)
                    {
                        if (!DisplayRadar.Option.IsOnlyEnemyVisible || DisplayRadar.Option.IsOnlyEnemyVisible && _localPlayer.TeamId != player.TeamId)
                            DisplayRadar.DrawPlayer(Render2D, player.Position, _localPlayer.Position, RenderSurface.Size, _localPlayer.Yaw, player.IsVisible, _localPlayer.TeamId == player.TeamId);
                    }
                }
            }

            if (Crosshair.Option.IsVisible)
                Crosshair.Draw(Render2D, RenderSurface.Size);
        }

        protected override void Loop()
        {
            if (!ProcessMemory.IsRunProcess())
            {
                ConsoleSpiner consoleSpiner = new ConsoleSpiner();
                Console.Write("Wait process ");
                while (!ProcessMemory.OpenProcess(Config.Game.Process))
                {
                    consoleSpiner.Turn();
                    Thread.Sleep(100);
                }
                Console.WriteLine("...");
                Console.WriteLine($"Found process: ID:{ProcessMemory.Process.Id}");
            }

            while (true)
            {
                Render2D.WindowRenderTarget.Resize(new Size2(RenderSurface.Size.Width, RenderSurface.Height));

                WriteMemory();
                ReadMemory();

                Render2D.BeginDraw();
                Render2D.Clear();

                Render2D.BrushColor = Color.Green;
                Render2D.DrawRectangle(new SharpDX.RectangleF(0, 0, 1920, 1080));
                Render2D.DrawLine(new Vector2(0, 0), new Vector2(1920, 1080));
                Render2D.DrawLine(new Vector2(1920, 0), new Vector2(0, 1080));

                Draw();

                Render2D.EndDraw();

                if (!ProcessMemory.IsRunProcess())
                    break;
            }
        }
    }
}