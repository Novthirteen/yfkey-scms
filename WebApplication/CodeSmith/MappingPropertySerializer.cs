using System;
using System.Reflection;
using System.Xml;
using System.Drawing.Design;
using CodeSmith.Engine;

namespace com.Sconit.CodeSmith
{
    public class MappingPropertySerializer : IPropertySerializer
    {
        public MappingPropertySerializer()
        {
        }

        /// <summary>
        /// This method will be used to save the property value when a template is being compiled.
        /// </summary>
        /// <param name="propertyInfo">Information about the target property.</param>
        /// <param name="propertyValue">The property to be saved.</param>
        /// <returns>An object that will be stored in a Hashtable during template compilation.</returns>
        public object SaveProperty(PropertySerializerContext propertySerializerContext, object propertyValue)
        {
            // Nothing special needs to be done to save this property so we just return the unmodified property value.
            return propertyValue;
        }

        /// <summary>
        /// This method will be used to restore the property value after a template has been compiled.
        /// </summary>
        /// <param name="propertyInfo">Information about the target property.</param>
        /// <param name="propertyValue">The property to be loaded.</param>
        /// <returns>The value to be assigned to the template property after it has been compiled.</returns>
        public object LoadProperty(PropertySerializerContext propertySerializerContext, object propertyValue)
        {
            // Nothing special needs to be done to load this property so we just return the unmodified property value.
            return propertyValue;
        }

        /// <summary>
        /// This method will be used when serializing the property value to an XML property set.
        /// </summary>
        /// <param name="propertyInfo">Information about the target property.</param>
        /// <param name="writer">The XML writer that the property value will be written to.</param>
        /// <param name="propertyValue">The property to be serialized.</param>
        public void WritePropertyXml(PropertySerializerContext propertySerializerContext, System.Xml.XmlWriter writer, object propertyValue)
        {
            if (propertyValue == null) return;

            MappingProperty mappingPropertyValue = propertyValue as MappingProperty;
            if (mappingPropertyValue != null)
            {
                writer.WriteStartElement("MappingInfoCollection");

                foreach (MappingInfo mappingInfo in mappingPropertyValue.MappingInfoCollection)
                {
                    writer.WriteStartElement("MappingInfo");

                    writer.WriteElementString("IsPK", mappingInfo.IsPK.ToString());
                    writer.WriteElementString("IsUnique", mappingInfo.IsUnique.ToString());
                    writer.WriteElementString("PKGenerator", mappingInfo.PKGenerator);
                    writer.WriteElementString("ClassPropertyName", mappingInfo.ClassPropertyName);
                    writer.WriteElementString("TableColumnName", mappingInfo.TableColumnName);
                    writer.WriteElementString("DataType", mappingInfo.DataType);
                    writer.WriteElementString("DataLength", mappingInfo.DataLength.ToString());
                    writer.WriteElementString("IsNullable", mappingInfo.IsNullable.ToString());
                    writer.WriteElementString("PKMany2OnePropertyName", mappingInfo.PKMany2OnePropertyName);
                    writer.WriteElementString("PKMany2OnePropertyDataType", mappingInfo.PKMany2OnePropertyDataType);
                    writer.WriteElementString("IsOne2Many", mappingInfo.IsOne2Many.ToString());
                    writer.WriteElementString("One2ManyTable", mappingInfo.One2ManyTable);
                    writer.WriteElementString("One2ManyColumn", mappingInfo.One2ManyColumn);
                    writer.WriteElementString("One2ManyInverse", mappingInfo.One2ManyInverse.ToString());
                    writer.WriteElementString("One2ManyLazy", mappingInfo.One2ManyLazy.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// This method will be used when deserializing the property from an XML property set.
        /// </summary>
        /// <param name="propertyInfo">Information about the target property.</param>
        /// <param name="propertyValue">The XML node to read the property value from.</param>
        /// <param name="basePath">The path to use for resolving file references.</param>
        /// <returns>The value to be assigned to the template property.</returns>
        public object ReadPropertyXml(PropertySerializerContext propertySerializerContext, System.Xml.XmlNode propertyValue)
        {
            if (propertySerializerContext.PropertyInfo.PropertyType == typeof(MappingProperty))
            {
                MappingProperty mappingPropertyValue = new MappingProperty();

                XmlNodeList nodes = propertyValue.SelectNodes("MappingInfoCollection/MappingInfo");

                foreach (XmlNode node in nodes)
                {
                    MappingInfo mappingInfo = new MappingInfo();

                    mappingInfo.IsPK = bool.Parse(node.SelectSingleNode("IsPK").InnerText);
                    mappingInfo.IsUnique = bool.Parse(node.SelectSingleNode("IsUnique").InnerText);
                    mappingInfo.PKGenerator = node.SelectSingleNode("PKGenerator").InnerText;
                    mappingInfo.ClassPropertyName = node.SelectSingleNode("ClassPropertyName").InnerText;
                    mappingInfo.TableColumnName = node.SelectSingleNode("TableColumnName").InnerText;
                    mappingInfo.DataType = node.SelectSingleNode("DataType").InnerText;
                    mappingInfo.DataLength = int.Parse(node.SelectSingleNode("DataLength").InnerText);
                    mappingInfo.PKMany2OnePropertyName = node.SelectSingleNode("PKMany2OnePropertyName").InnerText;
                    mappingInfo.PKMany2OnePropertyDataType = node.SelectSingleNode("PKMany2OnePropertyDataType").InnerText;
                    mappingInfo.IsOne2Many = bool.Parse(node.SelectSingleNode("IsOne2Many").InnerText);
                    mappingInfo.One2ManyTable = node.SelectSingleNode("One2ManyTable").InnerText;
                    mappingInfo.One2ManyColumn = node.SelectSingleNode("One2ManyColumn").InnerText;
                    mappingInfo.One2ManyInverse = bool.Parse(node.SelectSingleNode("One2ManyInverse").InnerText);
                    mappingInfo.One2ManyLazy = bool.Parse(node.SelectSingleNode("One2ManyLazy").InnerText);

                    mappingPropertyValue.MappingInfoCollection.Add(mappingInfo);
                }

                return mappingPropertyValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will be used to parse a default value for a property when a template is being instantiated.
        /// </summary>
        /// <param name="propertyInfo">Information about the target property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="basePath">The path to use for resolving file references.</param>
        /// <returns>An object that will be assigned to the template property.</returns>
        public object ParseDefaultValue(PropertySerializerContext propertySerializerContext, string defaultValue)
        {
            return null;
        }
    }
}
