function test(){
    return 228;
}
mp.events.add("executeJS", (code, id) => {
    let result = eval(code)
    if(id != undefined) mp.events.callLocal("executeJSCallback", id, JSON.stringify(result));
});