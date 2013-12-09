using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Rain_On_Your_Parade
{
    class LevelParser
    {
        LevelParser()
        {
        }

        public static Canvas parse(string file)
        {
            XmlReader reader = XmlReader.Create("Content\\Levels\\" + file);
            List<Actor> actors = new List<Actor>();
            List<WorldObject> objects = new List<WorldObject>();
            Player player = null;
            int height = 0;
            int width = 0;
            int malice = 0;
            int x = 0;
            int y = 0;
            ActorType.Type atype = ActorType.Type.Cat;
            ObjectType.Type otype = ObjectType.Type.Chalking;
            GameEngine.WinCondition cond = GameEngine.WinCondition.Actors;
            int water = 0;

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "level": break;
                    case "objects":
                        while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "objects")
                        {
                            reader.Read();
                            if (reader.Name == "object")
                            {
                                while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "object")
                                {
                                    reader.Read();
                                    switch (reader.Name)
                                    {
                                        case "type":
                                            switch (reader.ReadInnerXml())
                                            {
                                                case "Chalking": otype = ObjectType.Type.Chalking;
                                                    break;
                                                case "SunnySpot": otype = ObjectType.Type.SunnyRainbowSpot;
                                                    break;
                                                case "Garden": otype = ObjectType.Type.Garden;
                                                    break;
                                                case "Pool": otype = ObjectType.Type.Pool;
                                                    break;
                                                case "House": otype = ObjectType.Type.House;
                                                    break;
                                                case "Laundry": otype = ObjectType.Type.Laundry;
                                                    break;
                                                default: break;
                                            }
                                            break;
                                        case "position":
                                            while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "position")
                                            {
                                                reader.Read();
                                                switch (reader.Name)
                                                {
                                                    case "x": x = int.Parse(reader.ReadInnerXml());
                                                        break;
                                                    case "y": y = int.Parse(reader.ReadInnerXml());
                                                        break;
                                                    default: break;
                                                }
                                            }
                                            break;
                                        case "water": water = int.Parse(reader.ReadInnerXml());
                                            break;
                                        default: break;
                                    }
                                }
                                objects.Add(new WorldObject(otype, new Microsoft.Xna.Framework.Point(x, y), water));
                            }
                        }
                        break;
                    case "actors":
                        while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "actors")
                        {
                            reader.Read();
                            if (reader.Name == "actor")
                            {
                                while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "actor")
                                {
                                    reader.Read();
                                    switch (reader.Name)
                                    {
                                        case "type":
                                            switch (reader.ReadInnerXml())
                                            {
                                                case "Cat": atype = ActorType.Type.Cat;
                                                    break;
                                                case "Kid": atype = ActorType.Type.Kid;
                                                    break;
                                                case "Mom": atype = ActorType.Type.Mom;
                                                    break;
                                                default: break;
                                            }
                                            break;
                                        case "position":
                                            while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "position")
                                            {
                                                reader.Read();
                                                switch (reader.Name)
                                                {
                                                    case "x": x = int.Parse(reader.ReadInnerXml());
                                                        break;
                                                    case "y": y = int.Parse(reader.ReadInnerXml());
                                                        break;
                                                    default: break;
                                                }
                                            }
                                            break;
                                        default: break;
                                    }
                                }
                                actors.Add(new Actor(atype, new Microsoft.Xna.Framework.Point(x, y)));
                            }
                        }
                        break;
                    case "targetMalice": malice = int.Parse(reader.ReadInnerXml());
                        break;
                    case "size":
                        while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "size")
                        {
                            reader.Read();
                            switch (reader.Name)
                            {
                                case "width": width = int.Parse(reader.ReadInnerXml());
                                    break;
                                case "height": height = int.Parse(reader.ReadInnerXml());
                                    break;
                                default: break;
                            }
                        }
                        break;
                    case "player":

                        while (reader.NodeType != XmlNodeType.EndElement || reader.Name  != "player")
                        {
                            reader.Read();
                            switch (reader.Name)
                            {
                                case "initialRain": water = int.Parse(reader.ReadInnerXml());
                                    break;
                                case "position":
                                    while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "position")
                                    {
                                        reader.Read();
                                        switch (reader.Name)
                                        {
                                            case "x": x = int.Parse(reader.ReadInnerXml());
                                                break;
                                            case "y": y = int.Parse(reader.ReadInnerXml());
                                                break;
                                            default: break;
                                        }
                                    }
                                    player = new Player(water);
                                    player.PixelPosition = new Microsoft.Xna.Framework.Vector2(x, y);
                                    break;
                            }
                        }
                        
                        break;
                    case "winCondition":
                        switch (reader.ReadInnerXml())
                        {
                            case "malice":
                                cond = GameEngine.WinCondition.Malice;
                                break;
                            case "actors":
                                cond = GameEngine.WinCondition.Actors;
                                break;
                            case "objects":
                                cond = GameEngine.WinCondition.Objects;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;

                }
            }

            return new Canvas(width, height, malice, cond, objects, actors, player);
        }
    }
}
