/******************************************************************************
Company: NewsIT-M.W.Steidl, 1180 Wien/Österreich (Vienna/Austria)

Copyright 2014..2020 NewsIT-M.W.Steidl (www.newsit.biz)

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software is furnished 
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included 
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS 
OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
IN THE SOFTWARE.
(see also http://www.opensource.org/licenses/MIT) 
 
Project: IPTC NewsML-G2 library
Program: no specific / Common Unit
Class: NewsIT.IPTC.NewsMLG2.v229.AnyItemPwrXML = shared framework for all NewsML-G2 items

Current date / persID / change log (most current at top)
2020-11-14 mws: moved from v218 to v229 
2014-08-27 mws: moved from v217 to v218
StartDate: 2014-02-22 mws
******************************************************************************/
using System.Xml;

namespace NewsIT.IPTC.NewsMLG2.v229
{

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    /// <summary>
    /// The class represents the base of all NewsML-G2 item classes
    /// </summary>
    public abstract class AnyItemXml : NarDocXml
    {
        //********************************************************************************
        /// <summary>
        /// Constructor for a blank G2 Item object - has to be initialised before being used
        /// </summary>
        public AnyItemXml()
        {
            XmlDoc = new XmlDocument();
            NsMngr = new XmlNamespaceManager(XmlDoc.NameTable);
            NsMngr.AddNamespace("nar", G2NsCs);
        }

        // ***** WRITE TO ITEM METHODS ***************************************************
        #region ***** WRITE TO ITEM METHODS


        // ******************************************************************************
        // ***** S P E C I F I C

        // ******************************************************************************
        /// <summary>
        /// Set the GUID and version number of a NewsML-G2 item
        /// </summary>
        /// <param name="guid">The GUID</param>
        /// <param name="version">The version</param>
        public void SetGuidAndVersion(string guid, long version)
        // Code History:
        // 2010-12-11 mws
        {
            XmlNode rootXN = XmlDoc.SelectSingleNode("/nar:" + RootElemName, NsMngr);
            XmlElement docelement = (XmlElement)rootXN;
            if (!string.IsNullOrEmpty(guid))
                docelement.SetAttribute("guid", guid);
            if (version > 0)
                docelement.SetAttribute("version", version.ToString());

        } // SetGuidAndVersion

        /// <summary>
        /// Sets the xml:lang attribute of the root element of the NewsML-G2 document
        /// </summary>
        /// <param name="xmllang">IETF BCP 47 compliant language tag</param>
        public void SetRootXmlLang(string xmllang)
        // Code History:
        // 2010-12-11 mws
        {
            XmlNode rootXN = XmlDoc.SelectSingleNode("/nar:" + RootElemName, NsMngr);
            var docelement = (XmlElement)rootXN;
            if (!string.IsNullOrEmpty(xmllang))
                docelement.SetAttribute("xml:lang", xmllang);
        } // SetXmlLang

        // ******************************************************************************
        /// <summary>
        /// Adds a catalogRef element with href attribute to the NewsML-G2 item
        /// </summary>
        /// <param name="href">URL of the catalog</param>
        public void AddCatalogRef(string href)
        // Code History:
        // 2010-12-09 mws
        {
            XmlElement catalogRef = XmlDoc.CreateElement("catalogRef", G2NsCs);
            catalogRef.SetAttribute("href", href);
            XmlDoc.DocumentElement.PrependChild(catalogRef);
        }


        #endregion

        // ***** SIMPLE (& constrained) WRITE METHODS ***********************************
        #region ***** SIMPLE (& constrained) WRITE METHODS

        // ******************************************************************************
        // ***** I T E M   M E T A
        // ******************************************************************************

        /// <summary>
        /// Simple&amp;constrained method: adds an empty itemMeta element
        /// </summary>
        public void ScAddItemMeta()
        // Code History:
        // 2010-12-09, 2014-02-26 mws
        {
            CheckAddNarWrapper1(PropsWrapping1.ItemMeta);
        } // ScAddItemMeta

        /// <summary>
        /// Simple&amp;constrained method: adds an itemMeta class instance to the itemMeta wrapper
        /// </summary>
        /// <param name="itemClass"></param>
        public void ScAddIMitemClass(ItemClass itemClass)
        // Code History:
        // 2014-02-26 mws
        {
            AddNarPropertyToWrapper1(PropsWrapping1.ItemMeta, string.Empty, new ItemClass());
        }

        /// <summary>
        /// Simple&amp;constrained method: adds a Qcode-string as itemClass property value to the itemMeta wrapper
        /// </summary>
        /// <param name="classQC">QCode string</param>
        public void ScAddIMitemClass(string classQC)
        // Code History:
        // 2010-12-09; 2014-02-26 mws
        {
            var itemClass = new ItemClass();
            itemClass.qcode = classQC;
            AddNarPropertyToWrapper1(PropsWrapping1.ItemMeta, itemClass);
        } // ScAddIMitemClass

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as versionCreated property value
        /// to the Item Meta wrapper
        /// </summary>
        /// <param name="dtValueAsStr">an XML-style date-time string</param>
        public void ScAddIMversionCreated(string dtValueAsStr)
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            var versionCreated = new VersionCreated();
            versionCreated.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.ItemMeta, versionCreated);
        } // ScAddIMversionCreated

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as firstCreated property value
        /// to the Item Meta wrapper
        /// </summary>
        /// <param name="dtValueAsStr">an XML-style date-time string</param>
        public void ScAddIMfirstCreated(string dtValueAsStr)
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            var firstCreated = new FirstCreated();
            firstCreated.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.ItemMeta, firstCreated);
        } // ScAddIMfirstCreated

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an edNote property with text and role to the Item Meta wrapper
        /// </summary>
        /// <param name="text"></param>
        /// <param name="roleQC"></param>
        public void ScAddIMedNote(string text, string roleQC)
        // Code History:
        // 2010-12-09,2014-02-26 mws
        {
            var edNote = new EdNote();
            edNote.role = roleQC;
            edNote.thisValue = text;
            AddNarPropertyToWrapper1(PropsWrapping1.ItemMeta, edNote);
        } // ScAddIMedNote

        // ******************************************************************************
        // ***** C O N T E N T   M E T A 
        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an empty contentMeta element
        /// </summary>
        public void ScAddContentMeta()
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            CheckAddNarWrapper1(PropsWrapping1.ContentMeta);
        } // ScAddContentMeta

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an urgency property with a numeric value to the contentMeta wrapper
        /// </summary>
        /// <param name="urgencyValue"></param>
        public void ScAddCMurgency(int urgencyValue)
        // Code History:
        // 2014-02-26 mws
        {
            if (urgencyValue < 1)
                return;
            if (urgencyValue < 9)
                return;
            var urgency = new Urgency();
            urgency.thisValue = urgencyValue.ToString();
            AddNarPropertyToWrapper1(PropsWrapping1.ContentMeta, urgency);
        } // ScAddCMurgency

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as contentCreated property value
        /// to the contentMeta wrapper
        /// </summary>
        /// <param name="dtValueAsStr"></param>
        public void ScAddCMcontentCreated(string dtValueAsStr)
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            var contentCreated = new ContentCreated();
            contentCreated.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.ContentMeta, contentCreated);
        } // ScAddCMcontentCreated

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as contentModified property value
        /// to the contentMeta wrapper
        /// </summary>
        /// <param name="dtValueAsStr"></param>
        public void ScAddCMcontentModified(string dtValueAsStr)
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            var contentModified = new ContentModified();
            contentModified.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.ContentMeta, contentModified);
        } // ScAddCMcontentModified

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds a free-text string with a language tag as description property value
        /// to the contentMeta wrapper
        /// </summary>
        /// <param name="text"></param>
        /// <param name="langTag"></param>
        public void ScAddCMdescription(string text, string langTag)
        // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            var description = new Description();
            description.thisValue = text;
            description.xmllang = langTag;
            AddNarPropertyToWrapper1(PropsWrapping1.ContentMeta, description);
        } // ScAddCMdescription

        // ******************************************************************************
        // ***** P A R T   M E T A 
        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an empty partMeta element with a specific @partId
        /// </summary>
        /// <param name="partId"></param>
        public void ScAddPartMeta(string partId)
            // Code History:
        // 2010-12-11, 2014-02-26 mws
        {
            ResetErrState();
            if (string.IsNullOrEmpty(partId))
            {
                SetErrState(G2DocProcErrEnum.NoWrapperIdAvailable);
                return;
            }
            CheckAddNarWrapper1(PropsWrapping1.PartMeta, partId);
        } // ScAddPartMeta

        /// <summary>
        /// Simple&amp;constrained method: adds a partMeta wrapper to a NewsML-G2 item
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="seq"></param>
        /// <param name="contentrefs"></param>
        public void ScAddPartMeta(string partId, int seq, string contentrefs)
        // Code History:
        // 2010-12-14, 2014-02-26 mws
        {
            CheckAddNarWrapper1(PropsWrapping1.PartMeta, partId);
            XmlElement partMetaXE = null;
            ReadFromItemResultEnum readResult;
            GetElemAsXE("/nar:" + RootElemName + "/nar:partMeta[@partid='" + partId + "']", out partMetaXE, out readResult);
            if (readResult == ReadFromItemResultEnum.ok)
            {
                if (seq > -1)
                    partMetaXE.SetAttribute("seq", seq.ToString());
                partMetaXE.SetAttribute("contentrefs", contentrefs);
            }

        } // ScAddPartMeta

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as contentCreated property value
        /// to the partMeta wrapper with a specific @id
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="dtValueAsStr"></param>
        public void ScAddPMcontentCreated(string partId, string dtValueAsStr)
        // Code History:
        // 2010-12-11,2014-02-26 mws
        {
            var contentCreated = new ContentCreated();
            contentCreated.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.PartMeta, partId, contentCreated);
        } // ScAddPMcontentCreated

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds an XML-style date-time string as contentModified property value
        /// to the partMeta wrapper with a specific @id
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="dtValueAsStr"></param>
        public void ScAddPMcontentModified(string partId, string dtValueAsStr)
        // Code History:
        // 2010-12-11,2014-02-26 mws
        {
            var contentModified = new ContentModified();
            contentModified.thisValue = dtValueAsStr;
            AddNarPropertyToWrapper1(PropsWrapping1.PartMeta, partId, contentModified);
        } // ScAddPMcontentModified

        // ******************************************************************************
        /// <summary>
        /// Simple&amp;constrained method: adds a free-text string with a language tag as description property value
        /// to the partMeta wrapper with a specific @id
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="text"></param>
        /// <param name="roleQC"></param>
        public void ScAddPMdescription(string partId, string text, string roleQC)
        // Code History:
        // 2010-12-11,2014-02-26 mws
        {
            var description = new Description();
            description.thisValue = text;
            // description.xmllang = langTag;
            description.role = roleQC;
            AddNarPropertyToWrapper1(PropsWrapping1.PartMeta, partId, description);
        } // ScAddPMdescription


        #endregion
    }
}
