﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;


namespace Plugin
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        public override string Author => "Ak";

        public override string Description => "展示装备栏";

        public override string Name => "Show Armors";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public Plugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {

            Commands.ChatCommands.Add(new Command(
                //permissions: new List<string> { },
                cmd: this.ShowMySlots,
                "装备", "show", "zb")
            {
                HelpText = "发送自己的装备配置到聊天框。别名：show 和 zb "
            });
        }

        private void ShowMySlots(CommandArgs args)
        {
            TSPlayer target = null;
            Item[] armors = null;
            string str = "";
            const int MAX_SLOTS_NUMBER = 10;
            int argsCount = args.Parameters.Count;
            bool nothingEpuipped = false;

            if (argsCount != 0 && argsCount != 1)
            {
                args.Player.SendErrorMessage($"语法错误！正确语法 [c/55D284:/装备] [c/55D284:<玩家>]");
            }
            else if (argsCount == 0)
            {
                target = args.Player;
                armors = target.TPlayer.armor;
                str = $"{target.Name}" + " : ";
            }
            else if (argsCount == 1)
            {
                var players = TSPlayer.FindByNameOrID(args.Parameters[0]);
                if (players.Count == 0)
                {
                    args.Player.SendErrorMessage("不存在该玩家!");
                }
                else if (players.Count > 1)
                {
                    args.Player.SendMultipleMatchError(players.Select(p => p.Name));
                }
                else
                {
                    target = players[0];
                    armors = target.TPlayer.armor;
                    str = $"{target.Name}" + " : " + "拿着 " + $"[i/p{target.SelectedItem.prefix}:{target.SelectedItem.netID}]" + $"{(ItemPrefix)target.SelectedItem.prefix}";
                }
            }
            for (int i = 0; i < MAX_SLOTS_NUMBER; i++)
            {
                bool isArmor = i < 3;
                bool isAccessories = i < MAX_SLOTS_NUMBER;
                if (armors[i] == null || armors[i].netID == 0)
                {
                    continue;
                }
                else if (isArmor)
                {
                    str += $"[i:{armors[i].netID}]";
                    continue;
                }
                else if (isAccessories)
                {
                    str += $"[i/p{armors[i].prefix}:{armors[i].netID}]" + $"{(ItemPrefix)armors[i].prefix}" + " ";
                }
                else
                {
                    continue;
                }
            }

            nothingEpuipped = str == ($"{target.Name}" + " : ");
            if (argsCount == 0)
            {
                if (nothingEpuipped)
                {

                    TShock.Utils.Broadcast($"{target.Name}这家伙啥都没装备。" + "只拿着: " + $"[i/p{target.SelectedItem.prefix}:{target.SelectedItem.netID}]" + $"{(ItemPrefix)target.SelectedItem.prefix}", Microsoft.Xna.Framework.Color.Green);
                }
                else
                {
                    
                    TShock.Utils.Broadcast(str += " 拿着: " + $"[i/p{target.SelectedItem.prefix}:{target.SelectedItem.netID}]" + $"{(ItemPrefix)target.SelectedItem.prefix}", Microsoft.Xna.Framework.Color.Green);
                }
            }
            if (argsCount == 1)
            {
                if (nothingEpuipped)
                {
                    args.Player.SendSuccessMessage($"{target.Name}这家伙啥都没装备，只拿着 " + $"[i/p{target.SelectedItem.prefix}:{target.SelectedItem.netID}]" + $"{(ItemPrefix)target.SelectedItem.prefix}");
                    
                }
                else
                {
                    args.Player.SendSuccessMessage(str += " 拿着: " + $"[i/p{target.SelectedItem.prefix}:{target.SelectedItem.netID}]" + $"{(ItemPrefix)target.SelectedItem.prefix}");
                }
            } 
        }
        

        public enum ItemPrefix
        {
            无附魔,
            大,
            巨大,
            危险,
            凶残,

            锋利,
            尖锐,
            微小,
            可怕,

            小,
            钝,
            倒霉,
            笨重,

            可耻,
            重,
            轻,
            精准,

            迅速,
            急速远程,
            恐怖,
            致命远程,

            可靠,
            可畏,
            无力,
            粗笨,

            强大,
            神秘,
            精巧,
            精湛,

            笨拙,
            无知,
            错乱,
            威猛,

            禁忌,
            天界,
            狂怒,
            锐利,
            高端,
            强力,
            碎裂,
            破损,
            粗劣,
            迅捷魔法,
            致命,
            灵活,
            灵巧,
            残暴,
            缓慢,
            迟钝,
            呆滞,
            惹恼,
            凶险,
            狂躁,
            致伤,
            强劲,
            粗鲁,
            虚弱,
            无情,
            暴怒,
            神级,
            恶魔,
            狂热,
            坚硬,
            守护,
            装甲,
            护佑,
            奥秘,
            精确,
            幸运,
            锯齿,
            尖刺,
            愤怒,
            险恶,
            轻快,
            快速,
            急速,
            迅捷,
            狂野,
            鲁莽,
            勇猛,
            暴力,
            传奇,
            虚幻,
            神话
        }
    
    }
}