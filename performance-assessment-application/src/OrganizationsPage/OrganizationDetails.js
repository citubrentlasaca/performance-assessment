import React from "react";
import NavBar from "../Shared/NavBar";
import TopBarThree from "../Shared/TopBarThree";
import TopBarTwo from "../Shared/TopBarTwo";
import announcementPhoto from './Images/announcement.png';

function OrganizationDetails() {
    const employee = JSON.parse(localStorage.getItem("employeeData"));

    return (
        <NavBar>
            {employee.role === "Admin" ? <TopBarTwo /> : <TopBarThree />}
            <div className="announcement-page" style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", height: "100%" }}>
                <div className="announcement-content" style={{ textAlign: "center" }}>
                    <h1>Welcome to Company A!</h1>
                    <img
                        src={announcementPhoto}
                        style={{
                            width: "150px",
                            height: "150px",
                        }}
                        alt="Announcement"
                    />
                    <p>No announcement yet</p>
                </div>

                <div className="add-announcement-button" style={{ position: "absolute", bottom: "0", margin: "10px" }}>
                    <button style={{
                        borderRadius: "25px",
                        border: "1px solid rgba(39, 198, 217, 0.90)",
                        background: "linear-gradient(254deg, #0273FF 40.22%, #00C0FE 76.98%, #00C0FE 88.25%)",
                        color: "#fff",
                        padding: "10px 20px",
                        fontSize: "16px",
                        cursor: "pointer"
                    }}>
                        Add Announcement
                    </button>
                </div>
            </div>
        </NavBar>
    );
}

export default OrganizationDetails;
