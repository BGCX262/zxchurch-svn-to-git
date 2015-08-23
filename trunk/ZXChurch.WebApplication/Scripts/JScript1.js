
(function (obj, projId, subjId, optId) {
    var IndexId = AppPlatform.Survey.Digg.ProjectList[projId].OptIdObject[optId];
    AppPlatform.Survey.Digg.ProjectList[projId].OptListCount[optId].count++;
    AppPlatform.$("apps_svy_opt_count_" + projId + "_" + IndexId).innerHTML = AppPlatform.Survey.Digg.ProjectList[projId].OptListCount[optId].count;
    AppPlatform.$("apps_svy_opt_title_" + projId + "_" + IndexId).innerHTML = AppPlatform.$("apps_svy_opt_title_" + projId + "_" + IndexId).getAttribute("doneText");
    if (AppPlatform.Survey.Digg.ProjectList[projId].DiggMode == 0) {
        for (i = 0; i < AppPlatform.Survey.Digg.ProjectList[projId].OptIdArray.length; i++) {
            var IndexId = AppPlatform.Survey.Digg.ProjectList[projId].OptIdObject[AppPlatform.Survey.Digg.ProjectList[projId].OptIdArray[i]];
            var OptId = AppPlatform.Survey.Digg.ProjectList[projId].OptIdArray[i];
            try {
                AppPlatform.$("apps_svy_opt_title_" + projId + "_" + IndexId).innerHTML = AppPlatform.$("apps_svy_opt_title_" + projId + "_" + IndexId).getAttribute("doneText");
            } catch (e) {
            }
        }
    }
    
    var SubmitUrl = "http://page.vote.qq.com/survey.php?PjtID=" + projId;
    SubmitUrl += "&SubjID="; SubmitUrl += subjId; SubmitUrl += "&OptID=";
    SubmitUrl += optId;
    SubmitUrl += "&fmt=json";
    SubmitUrl += "&result=0";
    SubmitUrl += "&rdm=" + Math.random();
    AppPlatform.JsLoader.load(projId, SubmitUrl, function () {
        AppPlatform.Survey.Digg.ReceiveDiggResult(projId);
    });
})