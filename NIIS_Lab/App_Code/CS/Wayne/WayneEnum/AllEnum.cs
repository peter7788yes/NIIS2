public enum MyPowerEnum
{
    顯示 = 1,
	瀏覽 = 2,
	新增 = 3,
	修改 = 4,
	刪除 = 5,
	查詢 = 6,
	列印 = 7,
    上傳 = 8,
    下載 = 9,
	審核 = 10,
	特權 = 11 

}

public enum PublishStateEnum
{
    全部 = 0,
    上架 = 1,
    下架 = 2
}


public enum RoleTypeEnum
{
    角色 = 1,
    組織 = 2,
    角色組織 = 3
}


public enum RoleLevelEnum
{
    中央 = 1,
    區管中心 = 2,
    局 = 3,
    所 = 4
}


public enum OrgLevelEnum
{
    中央 = 1,
    區管中心 = 2,
    局 = 3,
    所 = 4
}


public enum PowerLogicType
{
    OR = 0,
    AND = 1
}


public enum MyHttpMethod
{
    GET = 1,
    POST = 2
}

//public enum RunStep
//{
//    從預覽列印註冊 = 1,
//    從匯出註冊 = 2,
//    運算中 = 3,
//    運算完成 = 4,
//    重複的運算 = 5,
//    失敗 = 6,
//}

public enum ApplyType
{
    預覽列印 = 1,
    匯出xls = 2
}

public enum RunStep
{
    註冊 = 1,
    運算中 = 2,
    運算完成 = 3,
    運算失敗 = 4
}
