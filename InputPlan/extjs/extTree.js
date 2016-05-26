Ext.namespace('Ext.RIDS');
/*
 * 현재 사용되는 곳
 * 공용문서 -  extTree.jsp
 */
Ext.RIDS.TreeCTL = function( obj )
{
	var self = this;
	//expandLevel= 1;
	//preloadLevel= 2;
	
	this.title= 'Default Tree';
	this.expandLevel = 0;
	this.expandSelect = true;
	this.border =  false;
	this.rootText= 'Root';
	this.fullload = obj.fullload;
	this.targettype = obj.targettype;
	this.checked = obj.checked;
	this.displayvalue = obj.displayvalue;
	this.displaycode= obj.displaycode;
	this.displaysort = obj.displaysort;
	
	this.rootID = '53168.57904.35180.30903';
	this.URL = '/enovia/extjs/callback/rtpTreeList.jsp?rel=' + obj.relation;
	
	this.TARGET = '/enovia/components/rtpCommonDocumentListSelectProcess.jsp?objectId=53168.57904.40417.4355';
	
	this.rootVisible = obj.rootVisible;
	
	
	
	this.scroll = 'both';

	Ext.RIDS.TreeCTL.superclass.constructor.call(this, obj);

	
	
	this.ID = obj.ID;

	var initComplete = false;	// .. 자아.. 초기화가 되었는지를 체크..
	var lastPath = getCookie( this.ID  );

	var loadtype = 'links';
	if( this.fullload == 'true' ) loadtype = 'children';	
	
	
    var store = Ext.create('Ext.data.TreeStore', {
		fields: ['text','id','order'],
		proxy:{type:'ajax',url:this.URL,reader:{type:'json',root: loadtype }, extraParams: { 'PATH' : lastPath, 'fullload' : this.fullload , 'type': this.targettype, 'checked' : this.checked, 'displayvalue' : this.displayvalue,'displaycode' : this.displaycode,'displaysort' : this.displaysort } },
		root:{text:this.rootTitle,id:this.rootID, order:'0'},
		folderSort: true,
		sorters: [{property: 'order',direction: 'ASC'}]
	});

    
    var rootvisible = true;
    if( this.rootVisible == 'false' )
    	rootvisible = false;
    
	var tree = new Ext.tree.TreePanel({ 
		//layout: 'anchor', <-- 요걸로 하면 스크롤바가.. ㅜ.ㅜ
		animate: false,
		region: 'center',
		width: obj.width,
		border : false,
		height: obj.height,
		rootVisible: rootvisible ,
		//text: 'aaa', 
		//rootVisible: this.rootVisible,
		//useArrows: true,
		frame: false,
		//bodyStyle: 'background-color: #DFE8F6',
		//바탕색을 바꾸고 싶으면.. css 를 바꿔야 한다..
		// baseCls 를 변경하고.. 관련 CSS를 추가정의를 해주면 된다고 한다..
		// 디테일한 항목은 나중에 필요시점에서 다시..
		//frame: true,
		//title: 'Default Tree',
		store : store
	}); 

	

	tree.on( 'itemclick', function( obj , record, item ) { saveCookie( record ); this.selectAction( record.data.id ); }, this );
	tree.on( 'resize', function( obj , width, height ) { tree.setSize( width, height ); }, this );
	tree.on( 'itemappend', function( obj , node, index, opts ) {try{var lev = node.getDepth();if( lev < this.expandLevel ) node.expand();}catch(e){}}, this );
	tree.on( 'afteritemexpand', function( node, index ) { 
		if( initComplete == false ) // 초기화가 안되어 있는 상황..
		{	
			if( lastPath != null && lastPath.indexOf( node.data.id ) != -1)
			{
				if( lastPath.indexOf( node.data.id + "/" ) == -1)	// 최종이다..
				{
					self.selectAction( node.data.id );	
					initialComplete();
				}
			}
			else 
				initialComplete();
		}
		else
		{
			saveCookie( node );
			self.selectAction( node.data.id );	
		}
	}); 
	this.add( tree );

	var selectFunction = '';

	var initialComplete = function(){
		initComplete = true;
	}

	var saveCookie = function( record ){
		if( self.ID == null ) return;
		var oids = record.getPath();
/*
		var oids = record.data.id;
		var node = record;
		while( node.parentNode )
		{
			oids += "___" + node.parentNode.data.id;
			node = node.parentNode;
		}
*/
		setCookieLT( self.ID, oids, 604800, '/');
		//store.proxy.extraParams[ this.ID ] = oids;
	}
	this.selectAction = function( objectid ){
		if( selectFunction == '' )
		{
			// 기본 Action..
		}
		else{
			selectFunction( objectid, this, self );
		}
	}; 
	this.setSize2 = function( width, height )
	{
		this.setSize( width, height );
		tree.setSize( width, height );
	}
	this.SELECT = function( func ){
		selectFunction = func;
	} 
	this.EXPAND = function(){
		
	}
}
Ext.extend( Ext.RIDS.TreeCTL, Ext.Panel,{  } );
