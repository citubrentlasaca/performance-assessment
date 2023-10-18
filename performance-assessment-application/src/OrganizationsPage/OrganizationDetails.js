import React from "react";
import NavBar from "../Shared/NavBar"
import TopBarThree from "../Shared/TopBarThree"
import TopBarTwo from "../Shared/TopBarTwo";


function OrganizationDetails() {
    const employee = JSON.parse(localStorage.getItem("employeeData"));

    return (
        <NavBar>
            {employee.role === "Admin" ? <TopBarTwo /> : <TopBarThree />}
        </NavBar>
    )
}

export default OrganizationDetails