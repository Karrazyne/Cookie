﻿using System.Collections.Generic;
using Cookie.API.Protocol.Network.Messages.Game.Inventory.Exchanges;
using Cookie.API.Protocol.Network.Types.Game.Data.Items;
using Cookie.API.Utils.IO;

namespace Cookie.API.Protocol.Network.Messages.Game.Inventory.Items
{
    public class ExchangeObjectsModifiedMessage : ExchangeObjectMessage
    {
        public new const ushort ProtocolId = 6533;

        public ExchangeObjectsModifiedMessage(List<ObjectItem> @object)
        {
            Object = @object;
        }

        public ExchangeObjectsModifiedMessage()
        {
        }

        public override ushort MessageID => ProtocolId;
        public List<ObjectItem> Object { get; set; }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short) Object.Count);
            for (var objectIndex = 0; objectIndex < Object.Count; objectIndex++)
            {
                var objectToSend = Object[objectIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var objectCount = reader.ReadUShort();
            Object = new List<ObjectItem>();
            for (var objectIndex = 0; objectIndex < objectCount; objectIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                Object.Add(objectToAdd);
            }
        }
    }
}