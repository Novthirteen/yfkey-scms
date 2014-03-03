
if(typeof(rdcjs)=="undefined")
_rdc=rdcjs={};_rdc.AjaxHelper=function(){}
_rdc.AjaxHelper.ajax=function(url){this.xmlhttp=null;this.resetData=function(){this.method="POST";this.queryStringSeparator="?";this.argumentSeparator="&";this.URLString="";this.encodeURIString=true;this.execute=false;this.element=null;this.elementObj=null;this.requestFile=url;this.vars=new Object();this.responseStatus=new Array(2);};this.resetFunctions=function(){this.onLoading=function(){};this.onLoaded=function(){};this.onInteractive=function(){};this.onCompletion=function(){};this.onError=function(){};this.onFail=function(){};};this.reset=function(){this.resetFunctions();this.resetData();};this.createAJAX=function(){try{this.xmlhttp=new ActiveXObject("Msxml2.XMLHTTP");}catch(e1){try{this.xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");}catch(e2){this.xmlhttp=null;}}
if(!this.xmlhttp){if(typeof XMLHttpRequest!="undefined"){this.xmlhttp=new XMLHttpRequest();}else{this.failed=true;}}};this.setVar=function(name,value){this.vars[name]=Array(value,false);};this.encVar=function(name,value,returnvars){if(true==returnvars){return Array(encodeURIComponent(name),encodeURIComponent(value));}else{this.vars[encodeURIComponent(name)]=Array(encodeURIComponent(value),true);}}
this.processURLString=function(string,encode){encoded=encodeURIComponent(this.argumentSeparator);regexp=new RegExp(this.argumentSeparator+"|"+encoded);varArray=string.split(regexp);for(i=0;i<varArray.length;i++){urlVars=varArray[i].split("=");if(true==encode){this.encVar(urlVars[0],urlVars[1]);}else{this.setVar(urlVars[0],urlVars[1]);}}}
this.createURLString=function(urlstring){if(this.encodeURIString&&this.URLString.length){this.processURLString(this.URLString,true);}
if(urlstring){if(this.URLString.length){this.URLString+=this.argumentSeparator+urlstring;}else{this.URLString=urlstring;}}
this.setVar("noCache",new Date().getTime());urlstringtemp=new Array();for(key in this.vars){if(false==this.vars[key][1]&&true==this.encodeURIString){encoded=this.encVar(key,this.vars[key][0],true);delete this.vars[key];this.vars[encoded[0]]=Array(encoded[1],true);key=encoded[0];}
urlstringtemp[urlstringtemp.length]=key+"="+this.vars[key][0];}
if(urlstring){this.URLString+=this.argumentSeparator+urlstringtemp.join(this.argumentSeparator);}else{this.URLString+=urlstringtemp.join(this.argumentSeparator);}}
this.runResponse=function(){eval(this.response);}
this.runAJAX=function(urlstring){if(this.failed){this.onFail();}else{this.createURLString(urlstring);if(this.element){this.elementObj=document.getElementById(this.element);}
if(this.xmlhttp){var self=this;if(this.method=="GET"){totalurlstring=this.requestFile+this.queryStringSeparator+this.URLString;this.xmlhttp.open(this.method,totalurlstring,true);}else{this.xmlhttp.open(this.method,this.requestFile,true);try{this.xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded")}catch(e){}}
this.xmlhttp.onreadystatechange=function(){switch(self.xmlhttp.readyState){case 1:self.onLoading();break;case 2:self.onLoaded();break;case 3:self.onInteractive();break;case 4:self.response=self.xmlhttp.responseText;self.responseXML=self.xmlhttp.responseXML;self.responseStatus[0]=self.xmlhttp.status;self.responseStatus[1]=self.xmlhttp.statusText;if(self.execute){self.runResponse();}
if(self.elementObj){elemNodeName=self.elementObj.nodeName;elemNodeName.toLowerCase();if(elemNodeName=="input"||elemNodeName=="select"||elemNodeName=="option"||elemNodeName=="textarea"){self.elementObj.value=self.response;}else{self.elementObj.innerHTML=self.response;}}
if(self.responseStatus[0]=="200"){self.onCompletion();}else{self.onError();}
self.URLString="";break;}};this.xmlhttp.send(this.URLString);}}};this.reset();this.createAJAX();}
if(typeof(rdcjs)=="undefined")
_rdc=rdcjs={};_rdc.JsonHelper=function(){}
_rdc.JsonHelper.isFloat=function(mixed_var){return parseFloat(mixed_var*1)!=parseInt(mixed_var*1,10);}
_rdc.JsonHelper.getType=function(mixed_var){var s=typeof mixed_var,name;var getFuncName=function(fn){var name=(/\W*function\s+([\w\$]+)\s*\(/).exec(fn);if(!name){return'(Anonymous)';}
return name[1];};if(s==='object'){if(mixed_var!==null){if(typeof mixed_var.length==='number'&&!(mixed_var.propertyIsEnumerable('length'))&&typeof mixed_var.splice==='function'){s='array';}
else if(mixed_var.constructor&&getFuncName(mixed_var.constructor)){name=getFuncName(mixed_var.constructor);if(name==='Date'){s='date';}
else if(name==='RegExp'){s='regexp';}
else if(name==='PHPJS_Resource'){s='resource';}}}else{s='null';}}
else if(s==='number'){s=_rdc.JsonHelper.isFloat(mixed_var)?'double':'integer';}
return s;}
_rdc.JsonHelper.toArrayJSONString=function(objArray){var a=['['],b,i,l=objArray.length,v;function p(s){if(b){a.push(',');}
a.push(s);b=true;}
for(i=0;i<l;i+=1){v=objArray[i];switch(typeof v){case'undefined':case'function':case'unknown':break;case'object':p(_rdc.JsonHelper.__toJSONStringByType(v));break;default:p(_rdc.JsonHelper.__toJSONStringByType(v));}}
a.push(']');return a.join('');};_rdc.JsonHelper.toBooleanJSONString=function(objBoolean){return String(objBoolean);};_rdc.JsonHelper.toDateJSONString=function(objDate){function f(n){return n<10?'0'+n:n;}
return'"'+objDate.getFullYear()+'-'+
f(objDate.getMonth()+1)+'-'+
f(objDate.getDate())+'T'+
f(objDate.getHours())+':'+
f(objDate.getMinutes())+':'+
f(objDate.getSeconds())+'"';};_rdc.JsonHelper.toNumberJSONString=function(objNumber){return isFinite(objNumber)?String(objNumber):"null";};_rdc.JsonHelper.toObjectJSONString=function(objObject){var a=['{'],b,i,v;function p(s){if(b){a.push(',');}
a.push(_rdc.JsonHelper.__toJSONStringByType(i),':',s);b=true;}
for(i in objObject){if(objObject.hasOwnProperty(i)){v=objObject[i];switch(typeof v){case'undefined':case'function':case'unknown':break;case'object':p(_rdc.JsonHelper.__toJSONStringByType(v));break;default:p(_rdc.JsonHelper.__toJSONStringByType(v));}}}
a.push('}');return a.join('');};_rdc.JsonHelper.__toJSONStringByType=function(v){switch(_rdc.JsonHelper.getType(v)){case"boolean":return _rdc.JsonHelper.toBooleanJSONString(v);case"date":return _rdc.JsonHelper.toDateJSONString(v);case"string":return _rdc.JsonHelper.toStringJSONString(v);case"integer":case"double":return _rdc.JsonHelper.toNumberJSONString(v);case"object":return _rdc.JsonHelper.toObjectJSONString(v);case"array":return _rdc.JsonHelper.toArrayJSONString(v);default:return"null";}};_rdc.JsonHelper.toStringJSONString=function(objString){var m={'\b':'\\b','\t':'\\t','\n':'\\n','\f':'\\f','\r':'\\r','"':'\\"','\\':'\\\\'};if(/["\\\x00-\x1f]/.test(objString)){return'"'+objString.replace(/([\x00-\x1f\\"])/g,function(a,b){var c=m[b];if(c){return c;}
c=b.charCodeAt();return'\\u00'+
Math.floor(c/16).toString(16)+
(c%16).toString(16);})+'"';}
return'"'+objString+'"';};_rdc.JsonHelper.parseJSON=function(objStr,hook){try{if(/^("(\\.|[^"\\\n\r])*?"|[,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t])+?$/.test(objStr)){var j=eval('('+objStr+')');if(typeof hook==='function'){function walk(v){if(v&&typeof v==='object'){for(var i in v){if(v.hasOwnProperty(i)){v[i]=walk(v[i]);}}}
return hook(v);}
return walk(j);}
return j;}}catch(e){}
throw new SyntaxError("parseJSON");};if(typeof(rdcjs)=="undefined")
_rdc=rdcjs={};_rdc.$=function(id){return"string"==typeof id?document.getElementById(id):id;};_rdc.a1=function(className,tag,elm){var testClass=new RegExp("(^|\\s)"+className+"(\\s|$)");var tag=tag||"*";var elm=elm||document;var elements=(tag=="*"&&elm.all)?elm.all:elm.getElementsByTagName(tag);var returnElements=[];var current;var length=elements.length;for(var i=0;i<length;i++){current=elements[i];if(testClass.test(current.className)){returnElements.push(current);}}
return returnElements;}
_rdc.a2=function(){this.obj=(arguments.length)?arguments[0]:window;return this;}
_rdc.a2.prototype.setInterval=function(func,msec){var i=_rdc.a2.getNew();var t=_rdc.a2.buildCall(this.obj,i,arguments);_rdc.a2.set[i].timer=window.setInterval(t,msec);return i;}
_rdc.a2.prototype.setTimeout=function(func,msec){var i=_rdc.a2.getNew();_rdc.a2.buildCall(this.obj,i,arguments);_rdc.a2.set[i].timer=window.setTimeout("_rdc.a2.callOnce("+i+");",msec);return i;}
_rdc.a2.prototype.clearInterval=function(i){if(!_rdc.a2.set[i])return;window.clearInterval(_rdc.a2.set[i].timer);_rdc.a2.set[i]=null;}
_rdc.a2.prototype.clearTimeout=function(i){if(!_rdc.a2.set[i])return;window.clearTimeout(_rdc.a2.set[i].timer);_rdc.a2.set[i]=null;}
_rdc.a2.set=new Array();_rdc.a2.buildCall=function(obj,i,args){var t="";_rdc.a2.set[i]=new Array();if(obj!=window){_rdc.a2.set[i].obj=obj;t="_rdc.a2.set["+i+"].obj.";}
t+=args[0]+"(";if(args.length>2){_rdc.a2.set[i][0]=args[2];t+="_rdc.a2.set["+i+"][0]";for(var j=1;(j+2)<args.length;j++){_rdc.a2.set[i][j]=args[j+2];t+=", _rdc.a2.set["+i+"]["+j+"]";}}
t+=");";_rdc.a2.set[i].call=t;return t;}
_rdc.a2.callOnce=function(i){if(!_rdc.a2.set[i])return;eval(_rdc.a2.set[i].call);_rdc.a2.set[i]=null;}
_rdc.a2.getNew=function(){var i=0;while(_rdc.a2.set[i])i++;return i;}
_rdc.a3=function(){}
_rdc.a3.Bind=function(object,fun){return function(){return fun.apply(object,arguments);}}
_rdc.a3.a25=function(object,fun){return function(event){return fun.call(object,(event||window.event));}}
_rdc.a3.addEvent=function(oTarget,sEventType,fnHandler){if(oTarget.addEventListener){oTarget.addEventListener(sEventType,fnHandler,false);}else if(oTarget.attachEvent){oTarget.attachEvent("on"+sEventType,fnHandler);}else{oTarget["on"+sEventType]=fnHandler;}};_rdc.a3.removeEvent=function(oTarget,sEventType,fnHandler){if(oTarget.removeEventListener){oTarget.removeEventListener(sEventType,fnHandler,false);}else if(oTarget.detachEvent){oTarget.detachEvent("on"+sEventType,fnHandler);}else{oTarget["on"+sEventType]=null;}};_rdc.a3.cancelEvent=function(e){e=e||window.event;if(e.preventDefault){e.preventDefault();e.stopPropagation();}else{e.returnValue=false;e.cancelBubble=true;}};_rdc.a3.getEventTarget=function(e){e=e||window.event;var obj=e.srcElement?e.srcElement:e.target;return obj};_rdc.a3.prev=function(elem){do{elem=elem.previousSibling;}while(elem&&elem.nodeType!=1);return elem;}
_rdc.a3.next=function(elem){do{elem=elem.nextSibling;}while(elem&&elem.nodeType!=1);return elem;}
_rdc.a3.first=function(elem){elem=elem.firstChild;return elem&&elem.nodeType!=1?_rdc.a3.next(elem):elem;}
_rdc.a3.last=function(elem){elem=elem.lastChild;return elem&&elem.nodeType!=1?_rdc.a3.prev(elem):elem;}
_rdc.a3.parent=function(elem,num){num=num||1;for(var i=0;i<num;i++)
if(elem!=null)
elem=elem.parentNode;return elem;}
_rdc.a3.trim=function(str){return str.replace(/^\s+|\s+$/g,"");}
_rdc.a3.ltrim=function(str){return str.replace(/^\s+/,"");}
_rdc.a3.rtrim=function(str){return str.replace(/\s+$/,"");}
_rdc.a3.purge=function(d){var a=d.attributes,i,l,n;if(a){l=a.length;for(i=0;i<l;i+=1){n=a[i].name;if(typeof d[n]==='function'){d[n]=null;}}}
a=d.childNodes;if(a){l=a.length;for(i=0;i<l;i+=1){purge(d.childNodes[i]);}}}
_rdc.a3.f1=function(el){var p=el.parentNode;while(p){if(p.getAttribute&&p.getAttribute("is-astreeview-node"))
return p;p=p.parentNode;}
return null;}
_rdc.a3.f3=function(str){return str.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;');}
_rdc.a3.f2=function(str){return str.replace(/&lt;/g,'<').replace(/&gt;/g,'>').replace(/&amp;/g,'&');}
_rdc.a3.a4=(document.all)?true:false;_rdc.a3.a46=_rdc.a3.a4&&([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1]==6);_rdc.a3.a48=_rdc.a3.a4&&([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1]==8);_rdc.a3.contains=function(arr,obj){var i=arr.length;while(i--){if(arr[i]===obj){return true;}}
return false;}
_rdc.ASTreeViewNode=function(){this.treeNodeText="";this.treeNodeValue="";this.checkedState=0;this.openState=0;this.selected=false;this.enableEditContextMenu=true;this.enableDeleteContextMenu=true;this.enableAddContextMenu=true;this.treeNodeType=0;this.treeNodeIcon="";this.enableDragDrop=true;this.enableSiblings=true;this.enableChildren=true;this.additionalAttributes="";this.isVirtualNode=false;this.virtualNodesCount=0;this.virtualParentKey="";this.treeNodeDepth=-1;}
_rdc.ASTreeViewLinkNode=function(){this.href="";this.target="";this.tooltip="";}
_rdc.ASTreeViewLinkNode.prototype=new _rdc.ASTreeViewNode;_rdc.ASTreeView=function(insName)
{this.f4=_rdc.a3.a25(this,this.f7);this.__d33=_rdc.a3.a25(this,this._d33);this._d35=_rdc.a3.a25(this,this.d35);this.f5=_rdc.a3.a25(this,this.f8);this._d42=_rdc.a3.a25(this,this.d42);this._d41=_rdc.a3.a25(this,this.d41);this._d46=_rdc.a3.a25(this,this.d46);this._d63=_rdc.a3.a25(this,this.d63);this._a20=_rdc.a3.a25(this,this.a20);this.__d52=_rdc.a3.a25(this,this._d52);this.__a22=_rdc.a3.a25(this,this._a22);this.__a23=_rdc.a3.a25(this,this._a23);this._a24=_rdc.a3.a25(this,this.a24);this.__c19=_rdc.a3.a25(this,this._c19);this.__d50=_rdc.a3.a25(this,this._d50);this.__d51=_rdc.a3.a25(this,this._d51);this.__c20=_rdc.a3.a25(this,this._c20);this.f6=_rdc.a3.a25(this,this.f9);this.timer=new _rdc.a2(this);this.b2='';this.b3='astreeview-folder.gif';this.b4='astreeview-folder-open.gif';this.b5='astreeview-node.gif';this.b6='astreeview-plus.gif';this.b7='astreeview-minus.gif';this.b8=6;this.b14="astreeview-checkbox-unchecked.gif";this.b15="astreeview-checkbox-checked.gif";this.b16="astreeview-checkbox-half-checked.gif";this.f25="astreeview-dragDrop-indicator1.gif";this.dragDripIndicatorSub="astreeview-dragDrop-indicator2.gif";if(!document.getElementById("b18")){this.b18=document.createElement('UL');this.b18.style.position='absolute';this.b18.style.display='none';this.b18.id='b18';this.b18.className="drag-container";document.body.appendChild(this.b18);}
else
this.b18=document.getElementById("b18");this.b21=false;this.az2=-1;this.dragNodeDisableSiblings=false;this.b29=false;if(document.all){this.indicatorOffsetX=2;this.indicatorOffsetXSub=4;this.indicatorOffsetY=16;}else{this.indicatorOffsetX=1;this.indicatorOffsetXSub=3;this.indicatorOffsetY=16;}
if(navigator.userAgent.indexOf('Opera')>=0){this.indicatorOffsetX=2;this.indicatorOffsetXSub=3;this.indicatorOffsetY=16;}
this.b25=insName;this.b81='';this.b26=true;this.b27=true;this.b31=false;this.b28=false;this.c9="Please enter name for the new node.";this.addNodePromptDefaultValue="";this.addNodeDataValueProvider=null;this.c10='AddNode.aspx';this.c11='EditNode.aspx';this.c12='DeleteNode.aspx';this.c13='Are you sure to delete {0}?';this.c14='Are you sure to delete {0}? It has sub nodes.';this.c15={};this.b84={};this.b85={};this.c16={};this.b13=new Array();this.b30=false;this.b301=1;this.b302=2;this.b303=3;this.b304=null;this.b32=null;this.b33="astreeview-node-selected";this.b34=false;this.b35=true;this.b36="";this.b37="";this.b39=false;this.f27=false;this.enableDragDrop=true;this.b41=true;this.b42="";this.c17="";this.c18="";this.b43="";this.b44="";this.b45="";this.b466=false;this.b47=[];this.b48=false;this.c21=false;this.f32=false;this.f34=true;this.f36=false;this.f38="<!--ast_ajax_start-->(.|\\\s)*?<!--ast_ajax_end-->";this.f40=false;this.f42=false;this.f44=false;this.b49=0;this.b50=function(){};this.b51=function(){};this.onDragDropStart=function(){};this.onDragDropComplete=function(){};this.c22=true;this.c23="LoadNodes.aspx";}
_rdc.ASTreeView.Consts={};_rdc.ASTreeView.Consts.c1=0;_rdc.ASTreeView.Consts.c11=1;_rdc.ASTreeView.Consts.c111=2;_rdc.ASTreeView.Consts.c1111=0;_rdc.ASTreeView.Consts.c11111=1;_rdc.ASTreeView.Consts.cc11=0;_rdc.ASTreeView.Consts.cc12=1;_rdc.ASTreeView.Consts.cc13=0;_rdc.ASTreeView.Consts.cc14=1;_rdc.ASTreeView.Consts.cc15=2;_rdc.ASTreeView.Consts.cc16=0;_rdc.ASTreeView.Consts.cc17=1;_rdc.ASTreeView.prototype={addEvent:function(e1,eventType,functionName)
{if(e1.attachEvent){e1['e'+eventType+functionName]=functionName;e1[eventType+functionName]=function(){e1['e'+eventType+functionName](window.event);}
e1.attachEvent('on'+eventType,e1[eventType+functionName]);}else
e1.addEventListener(eventType,functionName,false);},removeEvent:function(e1,eventType,functionName)
{if(e1.detachEvent){e1.detachEvent('on'+eventType,e1[eventType+functionName]);e1[eventType+functionName]=null;}else
e1.removeEventListener(eventType,functionName,false);},c2:function(name){var start=document.cookie.indexOf(name+"=");var len=start+name.length+1;if((!start)&&(name!=document.cookie.substring(0,name.length)))return null;if(start==-1)return null;var end=document.cookie.indexOf(";",len);if(end==-1)end=document.cookie.length;return unescape(document.cookie.substring(len,end));},c3:function(name,value,expires,path,domain,secure){expires=expires*60*60*24*1000;var today=new Date();var expires_date=new Date(today.getTime()+(expires));var cookieString=name+"="+escape(value)+
((expires)?";expires="+expires_date.toGMTString():"")+
((path)?";path="+path:"")+
((domain)?";domain="+domain:"")+
((secure)?";secure":"");document.cookie=cookieString;},c4:function(obj){var curleft=curtop=0;if(obj.offsetParent){do{curleft+=obj.offsetLeft;curtop+=obj.offsetTop;}while(obj=obj.offsetParent);}
return[curleft,curtop];},c5:function(obj){return this.c4(obj)[1];},c6:function(obj){return this.c4(obj)[0];},c7:function(e){var posx=0;if(!e)var e=window.event;if(e.pageX){posx=e.pageX;}
else if(e.clientX){posx=e.clientX+document.body.scrollLeft
+document.documentElement.scrollLeft;}
return posx;},c8:function(e){var posy=0;if(!e)var e=window.event;if(e.pageY){posy=e.pageY;}
else if(e.clientY){posy=e.clientY+document.body.scrollTop
+document.documentElement.scrollTop;}
return posy;},f12:function(val)
{this.c10=val;},f10:function(val){this.c9=val;},f11:function(val){this.addNodePromptDefaultValue=val;},f13:function(val)
{this.c11=val;},f16:function(val)
{this.c12=val;},f15:function(val){this.c13=val;},f14:function(val){this.c14=val;},f18:function(val){this.c15=val;},d3:function(val)
{this.b84=val;},d4:function(val)
{this.b85=val;},f17:function(val)
{this.c16=val;},d5:function(val)
{this.b26=val;},d6:function(val)
{this.b27=val;},d7:function(val)
{this.b8=val;},d8:function(val)
{this.b81=val;},d10:function(val)
{this.b3=val;},d11:function(val){this.b4=val;},d12:function(val){this.b5=val;},d13:function(val)
{this.b6=val;},d14:function(val)
{this.b7=val;},f19:function(val){this.b14=val;},f20:function(val){this.b15=val;},f21:function(val){this.b16=val;},f23:function(val){this.f25=val;},f22:function(val){this.f24=val;},d15:function(val)
{this.b1=val;},d16:function(val){this.b31=val;},d17:function(val){this.b33=val;},d18:function(val){this.b34=val;},d19:function(val){this.b37=val;},d20:function(val){this.b35=val;},d21:function(val){this.b36=val;},d22:function(val){this.b39=val;},f26:function(val){this.f27=val;},d23:function(val){this.enableDragDrop=val;},d24:function(val){this.b41=val;},d25:function(val){this.b42=val;},f28:function(val){this.c17=val;},f29:function(val){this.c18=val;},d26:function(val){this.b43=val;},d27:function(val){this.b44=val;},d28:function(val){this.b45=val;},d29:function(val){this.b47=val;},c24:function(val){this.b466=val;},c25:function(val){this.f30=val;},d30:function(val){this.b48=val;},c26:function(val){this.c21=val;},c27:function(val){this.c22=val;},c28:function(val){this.c23=val;},f31:function(val){this.f32=val;},f33:function(val){this.f34=val;},f35:function(val){this.f36=val;},f37:function(val){this.f38=val;},f39:function(val){this.f40=val;},f41:function(val){this.f42=val;},f43:function(val){this.f44=val;},d31:function()
{var menuItems=document.getElementById(this.b1).getElementsByTagName('LI');for(var i=0;i<menuItems.length;i++){var subItems=menuItems[i].getElementsByTagName('UL');if(subItems.length>0&&menuItems[i].getAttribute("openState")==_rdc.ASTreeView.Consts.c11111){this._d33(false,menuItems[i].id);}}},d32:function()
{var menuItems=document.getElementById(this.b1).getElementsByTagName('LI');for(var i=0;i<menuItems.length;i++){var subItems=menuItems[i].getElementsByTagName('UL');if(subItems.length>0&&menuItems[i].getAttribute("openState")==_rdc.ASTreeView.Consts.c1111){this._d33(false,menuItems[i].id);}}},f45:function()
{var menuItems=document.getElementById(this.b1).getElementsByTagName('LI');for(var i=0;i<menuItems.length;i++){var subItems=menuItems[i].getElementsByTagName('UL');if(subItems.length>0){this._d33(false,menuItems[i].id);}}},_d33:function(e,inputId)
{if(inputId){if(!document.getElementById(inputId))return;thisNode=this.getIcon(document.getElementById(inputId),_rdc.ASTreeView.Consts.cc13);}else{var evt=e||window.event;thisNode=evt.target||evt.srcElement;if(!thisNode.getAttribute('is-astreeview-node')){targetA=_rdc.a3.f1(thisNode);if(targetA)
thisNode=targetA;}
if(thisNode.tagName=='A')
thisNode=this.getIcon(thisNode.parentNode,_rdc.ASTreeView.Consts.cc13);}
if(thisNode.style.visibility=='hidden')return;var parentNode=thisNode.parentNode;inputId=parentNode.id;if(thisNode.src.indexOf(this.b6)>=0){thisNode.src=this.b7;var ul=parentNode.getElementsByTagName('UL')[0];ul.style.display='block';parentNode.setAttribute("openState",_rdc.ASTreeView.Consts.c1111)
if(this.c22){if(parentNode.getAttribute("is-virtual-node")=="true"){var obj=parentNode;var virtualParentKey=parentNode.getAttribute("virtual-parent-key");var ajaxIndex=this.b13.length;this.b13[ajaxIndex]=new _rdc.AjaxHelper.ajax();this.b13[ajaxIndex].method="GET";this.b13[ajaxIndex].setVar("virtualParentKey",virtualParentKey);this.__d62(this.b13[ajaxIndex],this.c16);this.b13[ajaxIndex].requestFile=this.c23;var ao=this.b13[ajaxIndex];var fError=this.f6;var fc20=this.__c20;ao.ajaxTrigger=obj;(function(){ao.onCompletion=function(){fc20(ao);};})();(function(){ao.onError=function(){fError(ajaxIndex,obj);};})();this._showAjaxIndicatorContainer();this.b13[ajaxIndex].runAJAX();}}}else{thisNode.src=this.b6;parentNode.getElementsByTagName('UL')[0].style.display='none';parentNode.setAttribute("openState",_rdc.ASTreeView.Consts.c11111)}
if(!this.c21){var folderIcon=this.getIcon(parentNode,_rdc.ASTreeView.Consts.cc15);if(folderIcon){if(parentNode.getAttribute("openState")==_rdc.ASTreeView.Consts.c11111)
folderIcon.src=this.b3;else if(parentNode.getAttribute("openState")==_rdc.ASTreeView.Consts.c1111)
folderIcon.src=this.b4;}}
if(e){if(this.b48)
this.d72();}
return false;},d34:function(inputId,state){if(!inputId||!_rdc.$(inputId))
return;var imgPlus=_rdc.$(inputId).getElementsByTagName('IMG')[0];if(imgPlus.style.visibility=='hidden')return;var liNode=imgPlus.parentNode;if(state==_rdc.ASTreeView.Consts.c1111){imgPlus.src=this.b7;var ul=liNode.getElementsByTagName('UL')[0];ul.style.display='block';}
else if(state==_rdc.ASTreeView.Consts.c11111){imgPlus.src=this.b6;liNode.getElementsByTagName('UL')[0].style.display='none';}},d35:function(e)
{var evt=e||window.event;var currentNode=evt.target||evt.srcElement;if(currentNode.getAttribute("treeNodeValue")&&this.f44){currentNode=currentNode.childNodes[0];}
else{if(!currentNode.getAttribute("is-astreeview-node"))
currentNode=_rdc.a3.f1(currentNode);if(!currentNode)
return;}
if(document.all)e=event;this.b18.style.left=this.c7(e)+'px';this.b18.style.top=this.c8(e)+'px';var subs=this.b18.getElementsByTagName('LI');if(subs.length>0){if(this.dragNodeSourceNextSib){this.dragNodeParent.insertBefore(this.dragNodeSource,this.dragNodeSourceNextSib);}else{this.dragNodeParent.appendChild(this.dragNodeSource);}}
this.dragNodeSource=currentNode.parentNode;this.dragNodeParent=currentNode.parentNode.parentNode;this.dragNodeSourceNextSib=false;if(this.dragNodeSource.nextSibling){this.dragNodeSourceNextSib=this.dragNodeSource.nextSibling;}
this.dragNodeDestination=false;this.appendToParent=false;this.az2=0;this.d36();_rdc.a3.cancelEvent(evt);return false;},geta2Counter:function(){return _rdc.a3.a4?25:40;},d36:function()
{var timerCounter=this.geta2Counter();if(this.az2>=0&&this.az2<timerCounter){this.az2=this.az2+1;this.timer.setTimeout('d36',10);return;}
if(this.az2==timerCounter)
{this.onDragDropStart(this.dragNodeSource);this.b18.style.display='block';this.b18.appendChild(this.dragNodeSource);}},f7:function(e)
{var timerCounter=this.geta2Counter();if(this.az2<timerCounter)return;if(document.selection)
document.selection.empty();else if(window.getSelection){window.getSelection().removeAllRanges();}
var evt=e||window.event;var curObj=evt.target||evt.srcElement;if(curObj.getAttribute("isTreeNodeChild"))
curObj=_rdc.a3.f1(curObj);if(document.all)e=event;dragDrop_x=this.c7(e)+5;dragDrop_y=this.c8(e)+5;if(!this.f42)
this.b18.style.left=dragDrop_x+'px';this.b18.style.top=dragDrop_y+'px';if(curObj.tagName=='A'||curObj.tagName=='IMG')
curObj=curObj.parentNode;this.dragNodeDisableSiblings=false;var tmpVar=curObj.getAttribute('enable-siblings');if(tmpVar&&tmpVar=='false')
this.dragNodeDisableSiblings=true;if(curObj&&curObj.id&&curObj.tagName=="LI")
{this.dragNodeDestination=curObj;this.appendToParent=false;var targetDepth=-1;var sourceDepth=-1;if(this.f40)
{targetDepth=parseInt(this.dragNodeDestination.getAttribute("tree-node-depth"));sourceDepth=parseInt(this.dragNodeSource.getAttribute("tree-node-depth"));if((sourceDepth-1)==targetDepth){this.appendToParent=true;}
else if(targetDepth!=sourceDepth){if(_rdc.a3.a4)
window.event.cancelBubble=true;else if(e)
e.stopPropagation();return;}}
var tmpObj=this.b20;tmpObj.style.display='block';var eventSourceObj=evt.target||evt.srcElement;if(eventSourceObj.getAttribute("isTreeNodeChild"))
eventSourceObj=_rdc.a3.f1(eventSourceObj);if(this.dragNodeDisableSiblings&&eventSourceObj.tagName=='IMG')eventSourceObj=eventSourceObj.nextSibling;var tmpImg=tmpObj.getElementsByTagName('IMG')[0];if((!this.f40&&(eventSourceObj.tagName=='A'||this.dragNodeDisableSiblings))||(this.appendToParent&&(eventSourceObj.tagName=='A'||this.dragNodeDisableSiblings))){var enableChildAttr=curObj.getAttribute("enable-children");if(enableChildAttr&&enableChildAttr.toLowerCase()=="false")
{if(_rdc.a3.a4)
window.event.cancelBubble=true;else if(e)
e.stopPropagation();return;}
if(tmpImg.src!=this.f24)
tmpImg.src=this.f24;this.b21=true;tmpObj.style.left=(this.c6(eventSourceObj)+this.indicatorOffsetXSub)+'px';tmpObj.style.top=(this.c5(curObj)+this.indicatorOffsetY)+'px';}else{if(this.f40){if(targetDepth!=sourceDepth){if(_rdc.a3.a4)
window.event.cancelBubble=true;else if(e)
e.stopPropagation();this.appendToParent=false;return;}
this.appendToParent=false;}
if(tmpImg.src!=this.f25)
tmpImg.src=this.f25;this.b21=false;var indicatorPos=this.c4(curObj);tmpObj.style.left=(indicatorPos[0]+this.indicatorOffsetX)+'px';tmpObj.style.top=(indicatorPos[1]+this.indicatorOffsetY)+'px';}}
return false;},f8:function(e)
{var evt=e||window.event;var currentObj=evt.target||evt.srcElement;var timerCounter=this.geta2Counter();if(this.az2<timerCounter){this.az2=-1;return;}
this.az2=-1;var showMessage=false;if(this.dragNodeDestination){var countUp=this.d40(this.dragNodeDestination,'up');var countDown=this.d40(this.dragNodeSource,'down');var countLevels=countUp/1+countDown/1+(this.b21?1:0);if(countLevels>this.b8){this.dragNodeDestination=false;showMessage=true;}}
var destnationParent=this.dragNodeDestination.parentNode;var isInTree=false;while(destnationParent)
{if(destnationParent.id&&(destnationParent.id==this.b1)){isInTree=true;break;}
destnationParent=destnationParent.parentNode;}
var relatedTreeToMove=false;var desParent=this.dragNodeDestination.parentNode;if(!isInTree){while(desParent)
{relatedTreeToMove=this.d74(desParent.id);if(relatedTreeToMove){isInTree=true;break;}
desParent=desParent.parentNode;}}
if(!isInTree||this.dragNodeDestination.tagName!="LI")
this.dragNodeDestination=false;if(this.f40)
{if(this.dragNodeDestination){var targetDepth=this.dragNodeDestination.getAttribute("tree-node-depth");var sourceDepth=this.dragNodeSource.getAttribute("tree-node-depth");if(targetDepth!=sourceDepth&&!this.appendToParent){this.dragNodeDestination=false;}}}
if(this.dragNodeDestination){if(this.b21){var uls=this.dragNodeDestination.getElementsByTagName('UL');if(uls.length>0){ul=uls[0];ul.style.display='block';var lis=ul.getElementsByTagName('LI');if(lis.length>0){ul.insertBefore(this.dragNodeSource,lis[0]);}else{ul.appendChild(this.dragNodeSource);}}else{var ul=document.createElement('UL');ul.style.display='block';this.dragNodeDestination.appendChild(ul);ul.appendChild(this.dragNodeSource);}
var imgPlusMinus=this.getIcon(this.dragNodeDestination,_rdc.ASTreeView.Consts.cc13);imgPlusMinus.style.visibility='visible';imgPlusMinus.src=this.b7;this.dragNodeDestination.setAttribute("openState",_rdc.ASTreeView.Consts.c1111);if(!this.c21){var imgIcon=this.getIcon(this.dragNodeDestination,_rdc.ASTreeView.Consts.cc15);if(imgIcon)
imgIcon.src=this.b4;}}else{if(this.dragNodeDestination.nextSibling){var nextSib=this.dragNodeDestination.nextSibling;nextSib.parentNode.insertBefore(this.dragNodeSource,nextSib);}else{this.dragNodeDestination.parentNode.appendChild(this.dragNodeSource);}}
if(this.b41){this.d69(this.dragNodeParent);this.d69(this.dragNodeSource.parentNode);for(var i=0;i<this.dragNodeSource.childNodes.length;i++){if(this.dragNodeSource.childNodes[i].tagName=="UL"){this.d69(this.dragNodeSource.childNodes[i]);}}
for(var i=0;i<this.dragNodeDestination.childNodes.length;i++){if(this.dragNodeDestination.childNodes[i].tagName=="UL"){this.d69(this.dragNodeDestination.childNodes[i]);}}
this.d70(this.dragNodeSource);if(this.dragNodeSource.parentNode&&this.dragNodeSource.parentNode.parentNode)
this.d70(this.dragNodeSource.parentNode.parentNode);if(this.dragNodeSource.parentNode){var newParentChildren=this.dragNodeSource.parentNode.childNodes;for(var i=0;i<newParentChildren.length;i++){if(newParentChildren[i].tagName=="LI")
this.d70(newParentChildren[i]);}}
var oldParentChildren=this.dragNodeParent.childNodes;for(var i=0;i<oldParentChildren.length;i++){if(oldParentChildren[i].tagName=="LI")
this.d70(oldParentChildren[i]);}
if(this.dragNodeParent.parentNode)
this.d70(this.dragNodeParent.parentNode);}
if(this.b31){this.d66(this.dragNodeSource);this.d66(this.dragNodeParent);}
var tmpObj=this.dragNodeParent;var lis=tmpObj.getElementsByTagName('LI');if(lis.length==0){var img=this.getIcon(tmpObj.parentNode,_rdc.ASTreeView.Consts.cc13);if(img)
img.style.visibility='hidden';if(!this.c21){var lastImgIcon=this.getIcon(tmpObj.parentNode,_rdc.ASTreeView.Consts.cc15);if(lastImgIcon)
lastImgIcon.src=this.b5;}
tmpObj.parentNode.removeChild(tmpObj);}
this.onDragDropComplete(this.dragNodeSource);}else{if(this.dragNodeSourceNextSib){this.dragNodeParent.insertBefore(this.dragNodeSource,this.dragNodeSourceNextSib);}else{this.dragNodeParent.appendChild(this.dragNodeSource);}}
this.b20.style.display='none';this.az2=-1;if(showMessage&&this.b81)alert(this.b81);if(document.all){currentObj.releaseCapture();}else{_rdc.a3.removeEvent(window,"blur",this.f5);};if(this.b48){this.d72();if(relatedTreeToMove)
eval(relatedTreeToMove+".d72();");}},d39:function()
{this.b20=document.createElement('DIV');this.b20.style.position='absolute';this.b20.style.zIndex='1000';this.b20.style.display='none';var img=document.createElement('IMG');img.src=this.f25;img.id='f25Image';this.b20.appendChild(img);document.body.appendChild(this.b20);},d40:function(obj,direction,stopAtObject){var countLevels=0;if(direction=='up'){while(obj.parentNode&&obj.parentNode!=stopAtObject){obj=obj.parentNode;if(obj.tagName=='UL')countLevels=countLevels/1+1;}
return countLevels;}
if(direction=='down'){var subObjects=obj.getElementsByTagName('LI');for(var i=0;i<subObjects.length;i++){countLevels=Math.max(countLevels,this.d40(subObjects[i],"up",obj));}
return countLevels;}},d41:function()
{return false;},d42:function()
{if(this.az2<15)return true;return false;},traverseTreeNode:function(fn,initObj){if(!initObj){initObj=document.getElementById(this.b1);}
var lis=initObj.getElementsByTagName('LI');if(lis&&lis.length>0){var li=lis[0];while(li){fn(li);var ul=li.getElementsByTagName('UL');if(ul.length>0)
this.traverseTreeNode(fn,ul[0]);li=li.nextSibling;}}},d43:function(initObj)
{var nodeArray=new Array();var isRoot=false;if(!initObj){isRoot=true;initObj=document.getElementById(this.b1);}
var lis;lis=initObj.getElementsByTagName('LI');if(lis&&lis.length>0){var li=lis[0];while(li){if(li.getAttribute&&li.getAttribute("treeNodeValue")){var nodeInfo;var treeNodeType=parseInt(li.getAttribute("treeNodeType"));if(treeNodeType==_rdc.ASTreeView.Consts.cc11)
nodeInfo=new _rdc.ASTreeViewNode();else if(treeNodeType==_rdc.ASTreeView.Consts.cc12)
nodeInfo=new _rdc.ASTreeViewLinkNode();else
continue;var curA=this.d75(li,"A");var liValue=li.getAttribute("treeNodeValue");var curCheckedState=li.getAttribute("checkedState");var curOpenState=li.getAttribute("openState");var curSelected=false;if(li.getAttribute("selected")&&li.getAttribute("selected")=="true")
curSelected=true;nodeInfo.treeNodeText=encodeURIComponent(curA.innerHTML);nodeInfo.treeNodeValue=encodeURIComponent(liValue);nodeInfo.checkedState=parseInt(curCheckedState);nodeInfo.openState=parseInt(curOpenState);nodeInfo.selected=curSelected;var tmpECM=li.getAttribute("enable-edit-context-menu");var curEditContextMenu=true;if(tmpECM&&tmpECM=="false")
curEditContextMenu=false;var tmpDCM=li.getAttribute("enable-delete-context-menu");var curDeleteContextMenu=true;if(tmpDCM&&tmpDCM=="false")
curDeleteContextMenu=false;var tmpACM=li.getAttribute("enable-add-context-menu");var curAddContextMenu=true;if(tmpACM&&tmpACM=="false")
curAddContextMenu=false;nodeInfo.enableEditContextMenu=curEditContextMenu;nodeInfo.enableDeleteContextMenu=curDeleteContextMenu;nodeInfo.enableAddContextMenu=curAddContextMenu;var tmpEDD=li.getAttribute("enable-drag-drop");var curEnableDragDrop=true;if(tmpEDD&&tmpEDD=="false")
curEnableDragDrop=false;var tmpES=li.getAttribute("enable-siblings");var curEnableSiblings=true
if(tmpES&&tmpES=="false")
curEnableSiblings=false;var tmpEC=li.getAttribute("enable-children");var curEnableChildren=true;if(tmpEC&&tmpEC=="false")
curEnableChildren=false;nodeInfo.enableDragDrop=curEnableDragDrop;nodeInfo.enableSiblings=curEnableSiblings;nodeInfo.enableChildren=curEnableChildren;nodeInfo.treeNodeType=treeNodeType;if(this.f40){if(li.getAttribute("tree-node-depth"))
nodeInfo.treeNodeDepth=parseInt(li.getAttribute("tree-node-depth"));}
if(li.getAttribute("is-virtual-node"))
nodeInfo.isVirtualNode=(li.getAttribute("is-virtual-node")=="true")?true:false;if(li.getAttribute("virtual-nodes-count"))
nodeInfo.virtualNodesCount=parseInt(li.getAttribute("virtual-nodes-count"));if(li.getAttribute("virtual-parent-key"))
nodeInfo.virtualParentKey=li.getAttribute("virtual-parent-key");if(li.getAttribute("additional-attributes"))
nodeInfo.additionalAttributes=li.getAttribute("additional-attributes");var ni=li.getAttribute("treeNodeIcon");if(ni){nodeInfo.treeNodeIcon=ni;}
if(treeNodeType==_rdc.ASTreeView.Consts.cc12){nodeInfo.href=curA.href;nodeInfo.target=curA.target;nodeInfo.tooltip=curA.title;}
nodeArray.push(nodeInfo);var ul=li.getElementsByTagName('UL');if(ul.length>0){nodeArray.push(this.d43(ul[0]));}}
li=li.nextSibling;}}
if(initObj.id==this.b1){return nodeArray;}
return nodeArray;},a24:function(inputObj,e)
{var evt=e||window.event;var elem=evt.target||evt.srcElement;if(this.b28)this.b28.className='';elem.className='highlighted-node-item';this.b28=elem;},d46:function()
{if(this.b28)this.b28.className='';this.b28=false;},d47:function(obj)
{var subs=obj.getElementsByTagName('LI');if(subs.length>0)return true;return false;},deleteItem:function(obj1,obj2)
{var message="";if(this.d47(obj2.parentNode))
message=this.c14.replace(/\{0\}/ig,obj2.innerHTML);else
message=this.c13.replace(/\{0\}/ig,obj2.innerHTML);if(confirm(message)){this._deleteItemStep2(obj2.parentNode);}},_d48:function(obj)
{if(this.d47(obj))return;var img=this.getIcon(obj,_rdc.ASTreeView.Consts.cc13);img.style.visibility='hidden';if(!this.c21){var folderIcon=this.getIcon(obj,_rdc.ASTreeView.Consts.cc15);if(folderIcon){folderIcon.src=this.b5;}}},_deleteItemStep2:function(obj)
{if(this.b466){var nValue=obj.getAttribute("treeNodeValue");var lis=obj.getElementsByTagName('LI');for(var i=0;i<lis.length;i++){nValue=nValue+','+lis[i].getAttribute("treeNodeValue");}
var ajaxIndex=this.b13.length;this.b13[ajaxIndex]=new _rdc.AjaxHelper.ajax();this.b13[ajaxIndex].method="GET";this.b13[ajaxIndex].setVar("deleteNodeValues",encodeURIComponent(nValue));this.__d62(this.b13[ajaxIndex],this.b85);this.b13[ajaxIndex].requestFile=this.c12;var ao=this.b13[ajaxIndex];var fError=this.f6;var fDeleteComplete=this.__d50;ao.ajaxTrigger=obj;(function(){ao.onCompletion=function(){fDeleteComplete(ao);};})();(function(){ao.onError=function(){fError(ajaxIndex,obj);};})();this._showAjaxIndicatorContainer();this.b13[ajaxIndex].runAJAX();}
else
{var parentRef=obj.parentNode.parentNode;obj.parentNode.removeChild(obj);this._d48(parentRef);if(this.b31)
this.manageNodeCheckboxState(parentRef);if(this.b48)
this.d72();}},f9:function(ajaxIndex,obj)
{alert("An error occured while requesting with ajax, please try again. ajaxIndex = "+ajaxIndex);this._hideAjaxIndicatorContainer();},_d50:function(ajaxObject)
{this._hideAjaxIndicatorContainer();if(ajaxObject.response!=_rdc.ASTreeView.Consts.cc16){alert('An error occured while deleting node: '+ajaxObject.response);}else{if(ajaxObject.ajaxTrigger){var parentRef=ajaxObject.ajaxTrigger.parentNode.parentNode;ajaxObject.ajaxTrigger.parentNode.removeChild(ajaxObject.ajaxTrigger);this._d48(parentRef);if(this.b31)
this.manageNodeCheckboxState(parentRef);ajaxObject.ajaxTrigger=null;}}},_d51:function(ajaxObject)
{this._hideAjaxIndicatorContainer();if(ajaxObject.response!=_rdc.ASTreeView.Consts.cc16){alert('An error occured while renaming node: '+ajaxObject.response);}},_showAjaxIndicatorContainer:function(){if(this.f30&&document.getElementById(this.f30))
document.getElementById(this.f30).style.display='';},_hideAjaxIndicatorContainer:function(){if(this.f30&&document.getElementById(this.f30))
document.getElementById(this.f30).style.display='none';},addItem:function(obj1,obj2){var curNode=obj2.parentNode;var curNodeValue=curNode.getAttribute("treeNodeValue");var newNodeText="";if(this.addNodeDataValueProvider)
newNodeText=this.addNodeDataValueProvider(curNode);else
newNodeText=window.prompt(this.c9,this.addNodePromptDefaultValue);if(!newNodeText||_rdc.a3.trim(newNodeText)==""||!curNodeValue)
return;if(this.f34)
newNodeText=_rdc.a3.f3(newNodeText);this._addItemStep2(newNodeText,curNodeValue,curNode);},_addItemStep2:function(newNodeText,parentNodeValue,obj){var ajaxIndex=this.b13.length;this.b13[ajaxIndex]=new _rdc.AjaxHelper.ajax();this.b13[ajaxIndex].method="GET";this.b13[ajaxIndex].setVar("addNodeText",encodeURIComponent(newNodeText));this.b13[ajaxIndex].setVar("parentNodeValue",encodeURIComponent(parentNodeValue));this.__d62(this.b13[ajaxIndex],this.c15);this.b13[ajaxIndex].requestFile=this.c10;var ao=this.b13[ajaxIndex];var fError=this.f6;var fAddComplete=this.__c19;ao.ajaxTrigger=obj;(function(){ao.onCompletion=function(){fAddComplete(ao);};})();(function(){ao.onError=function(){fError(ajaxIndex,obj);};})();this._showAjaxIndicatorContainer();this.b13[ajaxIndex].runAJAX();},_c19:function(ajaxObject)
{this._hideAjaxIndicatorContainer();var respRaw=String(ajaxObject.response);var resp="";if(this.f36){var regex=new RegExp(this.f38,"gi");if(regex.test(respRaw))
resp=respRaw.match(this.f38)[0];else
resp=respRaw;}
else
resp=respRaw;var liStr=resp.match(/<li[.\s\S(?!<li)]*?<\/li>/i);if(!liStr){alert('An error occured while adding node: '+ajaxObject.response);}else{if(ajaxObject.ajaxTrigger){var liParent=ajaxObject.ajaxTrigger;var imgPM=this.getIcon(liParent,_rdc.ASTreeView.Consts.cc13);if(imgPM){imgPM.style.visibility='visible';imgPM.src=this.b7;}
if(!this.c21){var folderIcon=this.getIcon(liParent,_rdc.ASTreeView.Consts.cc15);if(folderIcon){folderIcon.src=this.b4;}}
var div=document.createElement("div");div.innerHTML=String(liStr);var ulParent=this._getOrCreateUL(liParent);ulParent.style.display='block';for(var i=0;i<div.childNodes.length;i++){liNode=div.childNodes[i];ulParent.appendChild(liNode);this._d57Node(liNode);}
if(this.b41){this.d69(ulParent);for(var i=0;i<ulParent.childNodes.length;i++){if(ulParent.childNodes[i].tagName=="LI"){this.d70(ulParent.childNodes[i]);}}
if(ulParent.parentNode&&ulParent.parentNode.tagName=="LI")
this.d70(ulParent.parentNode);}
if(this.b31){var state=liParent.getAttribute("checkedState");if(state==_rdc.ASTreeView.Consts.c1){for(var i=0;i<ulParent.childNodes.length;i++){if(ulParent.childNodes[i].tagName=="LI")
this.d64(ulParent.childNodes[i],true);}
this.d66(liParent);}}
ajaxObject.ajaxTrigger=null;}}},_getOrCreateUL:function(li){for(var i=0;i<li.childNodes.length;i++){if(li.childNodes[i].tagName=="UL")
return li.childNodes[i];}
var newUL=document.createElement("ul");li.appendChild(newUL);return newUL;},_d52:function(e,inputObj)
{var evt=e||window.event;if(!inputObj&&this)inputObj=evt.target||evt.srcElement;if(document.all)e=event;if(e.keyCode&&e.keyCode==27){this.__a23(e,inputObj);return;}
inputObj.style.display='none';inputObj.nextSibling.style.display='';if(this.f34)
inputObj.value=_rdc.a3.f3(inputObj.value);if(inputObj.value.length>0){inputObj.nextSibling.innerHTML=inputObj.value;if(this.b466){if(this.b304!=this.b301){return;}
this.b304=this.b303;var ajaxIndex=this.b13.length;this.b13[ajaxIndex]=new _rdc.AjaxHelper.ajax();this.b13[ajaxIndex].method="GET";this.b13[ajaxIndex].setVar("nodeValue",encodeURIComponent(inputObj.parentNode.getAttribute("treeNodeValue")));this.b13[ajaxIndex].setVar("newNodeText",encodeURIComponent(inputObj.value));this.__d62(this.b13[ajaxIndex],this.b84);this.b13[ajaxIndex].requestFile=this.c11;this.b13[ajaxIndex].onError=function(){this.f6(ajaxIndex);};var ao=this.b13[ajaxIndex];var fEditComplete=this.__d51;(function(){ao.onCompletion=function(){fEditComplete(ao);};})();var fError=this.f6;(function(){ao.onError=function(){fError(ajaxIndex);};})();this._showAjaxIndicatorContainer();this.b13[ajaxIndex].runAJAX();}
else{if(this.b48)
this.d72();}}},_a23:function(e,inputObj)
{var evt=e||window.event;this.b304=this.b302;if(!inputObj&&this)inputObj=evt.target||evt.srcElement;inputObj.value=this.b30.innerHTML;inputObj.nextSibling.innerHTML=this.b30.innerHTML;inputObj.style.display='none';inputObj.nextSibling.style.display='';},_a22:function(e)
{if(document.all)e=event;var elem=e.srcElement||e.target;if(e.keyCode==13&&!this.f32){this._d52(false,elem);_rdc.a3.cancelEvent(e);}
if(e.keyCode==27){this._a23(false,elem);}},_d55:function(obj)
{var textBox=this.f32?document.createElement('TEXTAREA'):document.createElement('INPUT');textBox.className='astreeview-edit-box';textBox.value=this.f34?_rdc.a3.f2(obj.innerHTML):obj.innerHTML;obj.parentNode.insertBefore(textBox,obj);textBox.id='textBox'+obj.parentNode.getAttribute("treeNodeValue");_rdc.a3.addEvent(textBox,"blur",this.__d52);_rdc.a3.addEvent(textBox,"keydown",this.__a22);this._d56(obj);},_d56:function(obj)
{this.b304=this.b301;obj.style.display='none';obj.previousSibling.value=this.f34?_rdc.a3.f2(obj.innerHTML):obj.innerHTML;obj.previousSibling.style.display='inline';obj.previousSibling.focus();obj.previousSibling.select();},editItem:function(obj1,obj2)
{b29=obj2.parentNode;if(!obj2.previousSibling||obj2.previousSibling.tagName.toLowerCase()!='input'){this._d55(obj2);}else{this._d56(obj2);}
this.b30.innerHTML=obj2.innerHTML;},_c20:function(ajaxObject){this._hideAjaxIndicatorContainer();var respRaw=String(ajaxObject.response);var resp="";if(this.f36){var regex=new RegExp(this.f38,"gi");if(regex.test(respRaw))
resp=respRaw.match(this.f38)[0];else
resp=respRaw;}
else
resp=respRaw;if(ajaxObject.ajaxTrigger){var ulPlaceHolder=ajaxObject.ajaxTrigger.getElementsByTagName("ul")[0];if(ulPlaceHolder.getAttribute("virtial-node-placeholder-ul")=="true")
ulPlaceHolder.innerHTML=resp;this._initializeNodesUL(ulPlaceHolder);if(this.b41){this.d69(ulPlaceHolder);}
if(this.b31){var state=ajaxObject.ajaxTrigger.getAttribute("checkedState");if(state==_rdc.ASTreeView.Consts.c1){this.d64(ajaxObject.ajaxTrigger,true);}
this.manageNodeCheckboxState(ajaxObject.ajaxTrigger);}
ulPlaceHolder.parentNode.setAttribute("is-virtual-node","false");ajaxObject.ajaxTrigger=null;}},d57:function()
{this.d39();_rdc.a3.addEvent(document.documentElement,"selectstart",this._d42);_rdc.a3.addEvent(document.documentElement,"dragstart",this.d41);_rdc.a3.addEvent(document.documentElement,"mousedown",this._d46);this.b30=document.createElement('DIV');this.b30.style.display='none';document.body.appendChild(this.b30);if(this.b27||this.b26){try{}catch(e){}}
var astObj=document.getElementById(this.b1);this._initializeNodesUL(astObj);_rdc.a3.addEvent(document.documentElement,"mousemove",this.f4);_rdc.a3.addEvent(document.documentElement,"mouseup",this.f5);},_initializeNodesUL:function(ulObj){var menuItems=ulObj.getElementsByTagName('LI');for(var i=0;i<menuItems.length;i++){this._d57Node(menuItems[i]);}},_d57Node:function(liNode){if(liNode.getAttribute("virtial-node-placeholder-li")=="true")
return;var nodeEnableDragDrop=true;var tmpVar=liNode.getAttribute('enable-drag-drop');if(tmpVar&&tmpVar=='false')
nodeEnableDragDrop=false;var subItems=liNode.getElementsByTagName('UL');var imgPlusMinus=this.getIcon(liNode,_rdc.ASTreeView.Consts.cc13);_rdc.a3.addEvent(imgPlusMinus,"click",this.__d33);if(subItems.length>0)
{subItems[0].id='tree_ul_'+liNode.getAttribute("treeNodeValue");this.b49++;}
var aTag=liNode.getElementsByTagName('A')[0];if(nodeEnableDragDrop&&this.enableDragDrop){_rdc.a3.addEvent(aTag,"mousedown",this._d35);_rdc.a3.addEvent(aTag,"mousemove",this.f4);if(this.f44){_rdc.a3.addEvent(aTag.parentNode,"mousedown",this._d35);_rdc.a3.addEvent(aTag.parentNode,"mousemove",this.f4);}}
if(this.b31){var cbCheck=this.getIcon(liNode,_rdc.ASTreeView.Consts.cc14);_rdc.a3.addEvent(cbCheck,"click",this._d63);}
var folderImg=this.getIcon(liNode,_rdc.ASTreeView.Consts.cc15);if(folderImg){if(nodeEnableDragDrop)
_rdc.a3.addEvent(folderImg,"mousedown",this._d35);_rdc.a3.addEvent(folderImg,"mousemove",this.f4);}
if(this.b34){_rdc.a3.addEvent(aTag,"click",this._a20);}},__d58:function(curNode){if(curNode)
return curNode.getElementsByTagName("LI");else
return new Array();},__d59:function(curNode){var rawChildren=curNode.childNodes;var children=new Array();for(var count=0;count<rawChildren.length;count++){if(rawChildren[count].tagName&&rawChildren[count].tagName=="UL"){var rawLIs=rawChildren[count].childNodes;for(var i=0;i<rawLIs.length;i++){if(rawLIs[i].tagName&&rawLIs[i].tagName=="LI"){children.push(rawLIs[i]);}}}}
return children;},__d61:function(node){return this.__d60(node)},__d60:function(obj){var pNode=obj.parentNode;if(!pNode)
return null;if(pNode.id==this.b1)
return null;if(pNode.tagName=="LI")
return pNode;else
return this.__d60(pNode);},__d62:function(ajax,parameters)
{for(var parameter in parameters){ajax.setVar(parameter,parameters[parameter]);}},getCheckedNodesValues:function(includeHalfChecked,splitter){if(!splitter)
splitter=',';var result='';var traFunc=function(li){var state=li.getAttribute("checkedState");if(state)
state=parseInt(state);if((state==_rdc.ASTreeView.Consts.c1)||(state==_rdc.ASTreeView.Consts.c11&&includeHalfChecked)){var val=li.getAttribute("treeNodeValue");result+=(val+splitter);}}
this.traverseTreeNode(traFunc);if(result.length>0)
result=result.substr(0,result.length-splitter.length);return result;},d63:function(e){var evt=e||window.event;var elm=evt.target||evt.srcElement;if(elm.tagName!="IMG")
return;this.d64(elm);this.d66(elm.parentNode);if(this.b48)
this.d72();this.b51(e);},d64:function(elm,forceCheck){var nodeItem=(elm.tagName=="LI")?elm:elm.parentNode;if(elm.tagName=="LI")
elm=this.getIcon(elm,_rdc.ASTreeView.Consts.cc14);var state=nodeItem.getAttribute("checkedState");if(state!=_rdc.ASTreeView.Consts.c1||forceCheck){nodeItem.setAttribute("checkedState",_rdc.ASTreeView.Consts.c1);elm.src=this.b15;this.d65(nodeItem,_rdc.ASTreeView.Consts.c1);}
else{nodeItem.setAttribute("checkedState",_rdc.ASTreeView.Consts.c111);elm.src=this.b14;this.d65(nodeItem,_rdc.ASTreeView.Consts.c111);}},d65:function(targetNode,state){var childNodes=this.__d58(targetNode);for(var i=0;i<childNodes.length;i++){var curNode=childNodes[i];if(curNode.getAttribute("virtial-node-placeholder-li")=="true")
continue;var curCheckbox=this.getIcon(curNode,_rdc.ASTreeView.Consts.cc14);curNode.setAttribute("checkedState",state);if(state==_rdc.ASTreeView.Consts.c1)
curCheckbox.src=this.b15;else if(state==_rdc.ASTreeView.Consts.c111)
curCheckbox.src=this.b14;else if(state==_rdc.ASTreeView.Consts.c11)
curCheckbox.src=this.b16;}},manageNodeCheckboxState:function(node){while(node){if(node.id&&node.id==this.b1)
break;var children=this.__d59(node);var totalChildrenCount=children.length;var checkedNodesCount=0;var halfCheckedNodesCount=0;for(var i=0;i<children.length;i++){var curChildNode=children[i];if(curChildNode.getAttribute("checkedState")==_rdc.ASTreeView.Consts.c1)
checkedNodesCount++;if(curChildNode.getAttribute("checkedState")==_rdc.ASTreeView.Consts.c11)
halfCheckedNodesCount++;}
var curCheckbox=_rdc.a1("astreeview-checkbox","IMG",node)[0];if(halfCheckedNodesCount>0)
{node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c11);curCheckbox.src=this.b16;}
else{if(checkedNodesCount==0){node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c111);curCheckbox.src=this.b14;}
else if(checkedNodesCount==totalChildrenCount){node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c1);curCheckbox.src=this.b15;}
else{node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c11);curCheckbox.src=this.b16;}}
var node=this.__d61(node);}},d66:function(node){var pNode=this.__d61(node);this.manageNodeCheckboxState(pNode);},d67:function(){var astree=document.getElementById(this.b1);var children=astree.getElementsByTagName("li");if(children.length==0)
return;var root=children[0];this.d68(root);},d68:function(node){var children=this.__d59(node);if(children.length==0){return node.getAttribute("checkedState");}
else{var totalChildrenCount=children.length;var checkedNodesCount=0;var halfCheckedNodesCount=0;for(var i=0;i<children.length;i++){var curChildNode=children[i];var curState=this.d68(curChildNode);if(curState==_rdc.ASTreeView.Consts.c1)
checkedNodesCount++;if(curChildNode.getAttribute("checkedState")==_rdc.ASTreeView.Consts.c1&&this.__d59(curChildNode).length>0)
checkedNodesCount++;if(curChildNode.getAttribute("checkedState")==_rdc.ASTreeView.Consts.c11&&this.__d59(curChildNode).length>0)
halfCheckedNodesCount++;}
var curCheckbox=_rdc.a1("astreeview-checkbox","IMG",node)[0];if(halfCheckedNodesCount>0)
{node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c11);curCheckbox.src=this.b16;}
else{if(checkedNodesCount==0){node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c111);curCheckbox.src=this.b14;}
else if(checkedNodesCount==totalChildrenCount){node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c1);curCheckbox.src=this.b15;}
else{node.setAttribute("checkedState",_rdc.ASTreeView.Consts.c11);curCheckbox.src=this.b16;}}}},clearTreeLines:function(obj){var cn=obj.className;cn=cn.replace(this.b44,"");cn=cn.replace(this.c17,"");cn=cn.replace(this.c18,"");cn=cn.replace(this.b43,"");cn=cn.replace(this.b42,"");cn=cn.replace(this.b45,"");obj.className=cn;},isSingleRootNode:function(node){return(node.parentNode.childNodes.length==1)&&node.parentNode==document.getElementById(this.b1);},isTopRootNode:function(node){return(node.parentNode.childNodes.length>1&&node.parentNode==document.getElementById(this.b1)&&node==node.parentNode.firstChild);},d70:function(node){var parentUL=_rdc.a3.parent(node);if(!parentUL)
return;if(_rdc.a3.last(parentUL)==node){this.clearTreeLines(node);var str=node.className;str+=(" "+this.b44);node.className=str;}
else{this.clearTreeLines(node);var str=node.className;str+=(" "+this.b43);node.className=str;}
var childUL=_rdc.a3.last(node);if(childUL&&childUL.tagName=="UL")
this.d69(childUL);if(this.isSingleRootNode(node)){this.clearTreeLines(node);var str=node.className;str+=(" "+this.c17);node.className=str;}
if(this.isTopRootNode(node)){this.clearTreeLines(node);var str=node.className;str+=(" "+this.c18);node.className=str;}},d69:function(parentUL){if(parentUL.parentNode&&parentUL.parentNode.parentNode){var grandpa=parentUL.parentNode.parentNode
if(_rdc.a3.last(grandpa)==parentUL.parentNode){this.clearTreeLines(parentUL);this.clearTreeLines(parentUL.parentNode);var str=parentUL.className;str+=(" "+this.b45);parentUL.className=str;str=parentUL.parentNode.className;str+=(" "+this.b44);parentUL.parentNode.className=str;return;}}
this.clearTreeLines(parentUL);var str=parentUL.className;str+=(" "+this.b42);parentUL.className=str;},getSelectedNodesValue:function(){var result='';var traFunc=function(li){var state=li.getAttribute("selected");if(state=="true"){var val=li.getAttribute("treeNodeValue");result=val;}}
this.traverseTreeNode(traFunc);return result;},a20:function(e){var evt=e||window.event;var elm=evt.target||evt.srcElement;if(!elm.getAttribute('is-astreeview-node'))
elm=_rdc.a3.f1(elm);var node=elm.parentNode;if(!node)
return;if(this.f27&&this.__d59(node).length>0){var imgPlusMinus=this.getIcon(node,_rdc.ASTreeView.Consts.cc13);this.__d33(e,node.id);}
if(!this.b39){if(this.__d59(node).length>0)
return;}
this.d71();elm.className=this.b33+" "+elm.className;this.b32=node.id;node.setAttribute("selected","true");if(this.b48)
this.d72();this.b50(e);},d71:function(){var astree=document.getElementById(this.b1);var children=astree.getElementsByTagName("LI");for(var i=0;i<children.length;i++){children[i].removeAttribute("selected");var lastA=this.d75(children[i],"A");if(lastA)
lastA.className=lastA.className.replace(this.b33,"");}},d72:function(){if(this.b37!=""&&_rdc.$(this.b37))
_rdc.$(this.b37).value=_rdc.JsonHelper.toArrayJSONString(this.d43());},d73:function(){return this.b1;},d74:function(id){var result=false;for(var i=0;i<this.b47.length;i++){if(eval(this.b47[i]+".d73()")==id){return this.b47[i];break;}}
return result;},d75:function(parent,tag){for(var i=0;i<parent.childNodes.length;i++){if(parent.childNodes[i].tagName==tag)
return parent.childNodes[i]}
return null;},getIcon:function(parentObj,iconType){for(var i=0;i<parentObj.childNodes.length;i++){var cur=parentObj.childNodes[i];if(cur.tagName!="IMG")
continue;var it=cur.getAttribute("icon-type");if(!it)
continue;if(iconType==it)
return cur;}
return null;}}