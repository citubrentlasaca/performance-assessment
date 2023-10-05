import React from 'react'
import { Stack } from '@mui/material'
import TopBar from './TopBar'
import SideBar from './SideBar'

function NavBar({ children }) {
  return (
    <Stack
      direction="column"
      justifyContent="start"
      alignItems="flex-start"
      sx={{
        height: "100%",
        width: "100%",
        position: "fixed",
        top: 0,
        left: 0
      }}
    >
      <TopBar />
      <Stack
        direction="row"
        justifyContent="start"
        alignItems="flex-start"
        sx={{
          height: 'calc(100% - 100px)',
          width: "100%"
        }}
      >
        <SideBar />
        <Stack
          direction="column"
          justifyContent="flex-start"
          alignItems="center"
          sx={{
            height: "100%",
            width: "100%"
          }}
        >
          <div
            style={{
              height: '100%',
              width: '100%',
              overflowY: 'auto',
            }}
          >
            <main>
              {children}
            </main>
          </div>
        </Stack>
      </Stack>
    </Stack>
  )
}

export default NavBar