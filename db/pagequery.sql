DROP PROCEDURE IF EXISTS sp_page_query; #�ָ���  
CREATE PROCEDURE sp_page_query (  
    #�������  
    _fields VARCHAR(2000), #Ҫ��ѯ���ֶΣ��ö���(,)�ָ�  
    _tables TEXT,  #Ҫ��ѯ�ı�  
    _where VARCHAR(2000),   #��ѯ����  
    _orderby VARCHAR(200),  #�������  
    _pageindex INT,  #��ѯҳ��  
    _pageSize INT,   #ÿҳ��¼��  
    #�������  
    OUT _totalcount INT,  #�ܼ�¼��  
    OUT _pagecount INT    #��ҳ��  
)  
BEGIN  
    #140529-xxj-��ҳ�洢����  
    #������ʼ�к�  
    SET @startRow = _pageSize * (_pageIndex - 1);  
    SET @pageSize = _pageSize;  
    SET @rowindex = 0; #�к�  
  
    #�ϲ��ַ���  
    SET @strsql = CONCAT(  
        #'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #��¼�к�  
        'select sql_calc_found_rows '  
        ,_fields  
        ,' from '  
        ,_tables  
        ,CASE IFNULL(_where, '') WHEN '' THEN '' ELSE CONCAT(' where ', _where) END  
        ,CASE IFNULL(_orderby, '') WHEN '' THEN '' ELSE CONCAT(' order by ', _orderby) END  
      ,' limit '   
        ,@startRow  
        ,','   
        ,@pageSize  
    );  
  
    PREPARE strsql FROM @strsql;#����Ԥ�������   
    EXECUTE strsql;                         #ִ��Ԥ�������   
    DEALLOCATE PREPARE strsql;  #ɾ������   
    #ͨ�� sql_calc_found_rows ��¼û��ʹ�� limit ���ļ�¼��ʹ�� found_rows() ��ȡ����  
    SET _totalcount = FOUND_ROWS();  
  
    #������ҳ��  
    IF (_totalcount <= _pageSize) THEN  
        SET _pagecount = 1;  
    ELSE IF (_totalcount % _pageSize > 0) THEN  
        SET _pagecount = _totalcount / _pageSize + 1;  
    ELSE  
        SET _pagecount = _totalcount / _pageSize;  
    END IF;  
    END IF;    
END;  
  
##################################################  
# ���Դ洢����  
/*
select usr.*,gp.gp_code,gp.gp_name
from auth_user usr 
left join auth_group gp on usr.user_group=gp.gp_id
*/

CALL sp_page_query(  
'usr.*,gp.gp_code,gp.gp_name'#��ѯ�ֶ�  
,'auth_user usr left join auth_group gp on usr.user_group=gp.gp_id'#����  
,'1=1'#����  
,'gp.gp_sort asc'#����  
,1 #ҳ��  
,20 #ÿҳ��¼��  
,@totalcount #����ܼ�¼��  
,@pagecount #�����ҳ��  
);  
SELECT @totalcount,@pagecount;  