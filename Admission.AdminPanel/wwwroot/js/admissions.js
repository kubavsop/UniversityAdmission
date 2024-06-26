let query, newQuery;
$(document).ready(init);

function init() {
    $.ajaxSetup({
        contentType: 'application/json'
    });
    query = new Query($("[page-number]").val(), $("[program-name]").val(), $("[applicant-name]").val(), $("[admission-status]").val(), $("[without-manager]").val(), $("[sorting-options]").val(), $("[only-mine]").val(), $("[faculties]").val());
    newQuery = new Query(1, $("[program-name]").val(), $("[applicant-name]").val(), $("[admission-status]").val(), $("[without-manager]").val(), $("[sorting-options]").val(), $("[only-mine]").val(), $("[faculties]").val());
    setListeners();
}
function setListeners() {
    $("#search-button").on("click", () => { search() });
    $("[program-name]").on("input", (e) => {
        newQuery.EducationProgramName = $(e.target).val()
    });
    $("[applicant-name]").on("input", (e) => {
        newQuery.ApplicantName = $(e.target).val()
    });
    $("[admission-status]").on("input", (e) => {
        newQuery.AdmissionStatus = $(e.target).val()
    });
    $("[sorting-options]").on("input", (e) => {
        newQuery.SortingOptions = $(e.target).val()
    });
    $("[only-mine]").on("input", (e) => {
        newQuery.OnlyMine = $(e.target).val()
    });
    $("[without-manager]").on("input", (e) => {
        newQuery.WithoutManager = $(e.target).val()
    });
    $("[faculties]").on("input", (e) => {
        newQuery.Faculties = $(e.target).val()
    });
    $("[to-page]").on("click", (e) => {
        query.Page = $(e.target).attr("to-page");
        window.location.assign(baseUrl() + "/Admission/StudentAdmissions" + query.queryString());
    });
}

function search() {
    window.location.assign(baseUrl() + "/Admission/StudentAdmissions" + newQuery.queryString());
}

function baseUrl() {
    return window.location.protocol + "//" + window.location.host;
}

class Query {
    constructor(
        page = 1, 
        educationProgramName = null,
        applicantName = null,
        admissionStatus = null,
        withoutManager = null,
        sortingOptions = null,
        onlyMine = null,
        faculties = null) {
        this.Page = page;
        this.EducationProgramName = educationProgramName;
        this.ApplicantName = applicantName;
        this.AdmissionStatus = admissionStatus;
        this.WithoutManager = withoutManager;
        this.OnlyMine = onlyMine;
        this.SortingOptions = sortingOptions;
        this.Faculties = faculties;
    }
    queryString() {
        let result = "?"
        for (let attr in this) {
            if (this[attr]) {
                if (attr === "Faculties") {
                    console.log(this[attr])
                    this[attr].forEach(guid => {
                        result += `Faculties=${guid}&`;
                    });
                } else {
                    result += `${attr}=${this[attr]}&`
                }
            }
        }
        if (result.slice(-1) == '&' || result.slice(-1) == '?') result = result.slice(0, -1);
        return result;
    }
}