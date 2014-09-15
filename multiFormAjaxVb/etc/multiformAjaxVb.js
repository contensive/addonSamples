jQuery(document).ready(function(){
    jQuery('body').on('click','#mfaForm1 input[value=Next]',function(event){
        event.preventDefault();
        cj.remote({
            'method':'multiFormAjaxFormHandler'
            ,'formId':'mfaForm1'
            ,'destinationId':'multiFormAjaxFrame'
            ,'queryString':'ajaxButton=Next&multiformAjaxVbFrameRqs='+multiformAjaxVbFrameRqs
        });
    })
    jQuery('body').on('click','#mfaForm2 input[value=Next]',function(event){
        event.preventDefault();
        cj.remote({
            'method':'multiFormAjaxFormHandler'
            ,'formId':'mfaForm2'
            ,'destinationId':'multiFormAjaxFrame'
            ,'queryString':'ajaxButton=Next&multiformAjaxVbFrameRqs='+multiformAjaxVbFrameRqs
        });
    })
    jQuery('body').on('click','#mfaForm2 input[value=Previous]',function(event){
        event.preventDefault();
        cj.remote({
            'method':'multiFormAjaxFormHandler'
            ,'formId':'mfaForm2'
            ,'destinationId':'multiFormAjaxFrame'
            ,'queryString':'ajaxButton=Previous&multiformAjaxVbFrameRqs='+multiformAjaxVbFrameRqs
        });
    })
    jQuery('body').on('click','#mfaForm3 input[value=Previous]',function(event){
        event.preventDefault();
        cj.remote({
            'method':'multiFormAjaxFormHandler'
            ,'formId':'mfaForm3'
            ,'destinationId':'multiFormAjaxFrame'
            ,'queryString':'ajaxButton=Previous&multiformAjaxVbFrameRqs='+multiformAjaxVbFrameRqs
        });
    })
    jQuery('body').on('click','#mfaForm3 input[value=Finish]',function(event){
        event.preventDefault();
        cj.remote({
            'method':'multiFormAjaxFormHandler'
            ,'formId':'mfaForm3'
            ,'destinationId':'multiFormAjaxFrame'
            ,'queryString':'ajaxButton=Finish&multiformAjaxVbFrameRqs='+multiformAjaxVbFrameRqs
        });
    })
})